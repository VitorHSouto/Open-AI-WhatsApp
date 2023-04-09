using Microsoft.AspNetCore.Builder;
using System.Threading.Channels;
using VHS_Tarefas.Domain.Messages.CloudAPI;
using VHS_Tarefas.Entities;
using VHS_Tarefas.Enums;
using VHS_Tarefas.Gateway;
using VHS_Tarefas.Helpers;
using VHS_Tarefas.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace VHS_Tarefas.Services
{
    public class ChannelService : IChannelService
    {
        private ChannelRepository _channelRepository;
        private MessageService _messageService;
        private ContactChannelService _contactChannelService;
        private ChatContextService _chatContextService;
        public ChannelService(
            ChannelRepository channelRepository,
            MessageService messageService,
            ContactChannelService contactChannelService,
            ChatContextService chatContextService
        )
        {
            _channelRepository = channelRepository;
            _messageService = messageService;
            _contactChannelService = contactChannelService;
            _chatContextService = chatContextService;
        }

        public async Task HandleWebhook(CloudAPIWebhook webhook)
        {
            foreach (var entry in webhook.Entry)
            {
                foreach (var change in entry.Changes)
                {
                    if (change?.Value?.Messages != null)
                        await HandleNewMessage(change.Value);
                }
            }
        }

        private async Task HandleNewMessage(CloudAPIWebhookValue value)
        {
            var contactNumber = value?.Contacts?.FirstOrDefault()?.WaId;
            foreach(var message in value.Messages) 
                await HandleNewMessage(message, value.Metadata.DisplayPhoneNumber, contactNumber);
        }

        private async Task HandleNewMessage(CloudAPIMessage message, string channelNumber, string contactNumber)
        {
            try
            {
                var channel = await _channelRepository.GetByPhoneNumber(channelNumber);
                var contactChannel = await _contactChannelService.GetOrCreateByPhoneNumber(contactNumber);

                var query = message?.Text?.Body?.Trim();
                var gateway = GetGateway();

                if (string.IsNullOrWhiteSpace(query))
                {
                    await gateway.SendMessage("Não é possível extrair dados da midía selecionada, por favor digite um comando.");
                    await _chatContextService.CloseLastContext(channel.Id, contactChannel.Id);
                }
                else if (query.Contains("#fim"))
                {
                    await gateway.SendMessage("Conversa encerrada");
                    await _chatContextService.CloseLastContext(channel.Id, contactChannel.Id);
                }
                else if (query.Contains("/imagine"))
                {
                    var image = await OpenAIHelper.GenerateImageQuery(query.Replace("/imagine ", ""));
                    await gateway.SendImage(image);
                    await _chatContextService.CloseLastContext(channel.Id, contactChannel.Id);
                }
                else
                {
                    await AwserUserMessage(message, channel.Id, contactChannel.Id); 
                }
            }
            catch(Exception ex)
            {
                await SendErrorMessage(ex);
            }
        }

        private async Task AwserUserMessage(CloudAPIMessage message, Guid channelId, Guid contactChannelId)
        {
            var gateway = GetGateway();
            
            var messageIn = await _messageService.CreateByRole(message, channelId, contactChannelId, MessageRole.USER);
            var chatContextMessages = await _messageService.GetAllChatMessages(messageIn.ContextId);

            var answer = await OpenAIHelper.AnswerQuery(chatContextMessages);
            await gateway.SendMessage(answer);

            var answerMessage = new CloudAPIMessage();
            answerMessage.Text = new CloudAPIMessageText() { Body = answer };
            await _messageService.CreateByRole(answerMessage, channelId, contactChannelId, MessageRole.ASSISTANT);
        }

        #region CRUD

        public async Task<List<ChannelEntity>> ListAll()
        {
            return await _channelRepository.ListAll();
        }

        public async Task<ChannelEntity> Save(ChannelEntity entity)
        {
            return await _channelRepository.Insert(entity);
        }

        #endregion

        #region UTILS

        private async Task SendErrorMessage(Exception exception)
        {
            var gateway = GetGateway();

            var errorMessage = $"Não foi possível gerar uma resposta, por favor tente novamente em alguns segundos.";
            await gateway.SendMessage(errorMessage);
        }

        private IGateway GetGateway() 
        {
            return new CloudAPIGateway();
        }

        #endregion
    }
}
