using System.Threading.Channels;
using VHS_Tarefas.Domain.Messages.CloudAPI;
using VHS_Tarefas.Entities;
using VHS_Tarefas.Enums;
using VHS_Tarefas.Repositories;

namespace VHS_Tarefas.Services
{
    public class MessageService
    {
        private MessageRepository _messageRepository;
        private ChatContextService _chatContextService;
        public MessageService(
            MessageRepository messageRepository,
            ChatContextService chatContextService
            )
        {
            _messageRepository = messageRepository;
            _chatContextService = chatContextService;
        }

        public async Task<MessageEntity> CreateByRole(CloudAPIMessage message, Guid channelId, Guid contactChannelId, MessageRole role)
        {
            var entity = new MessageEntity();
            entity.Text = message?.Text?.Body;
            //TODO: Salvar Enum no banco
            entity.IsUser = role == MessageRole.USER;
            entity.IsBot = role == MessageRole.ASSISTANT;

            var chatContext = await _chatContextService.GetCurrentContext(channelId, contactChannelId);
            entity.ContextId = chatContext.Id;

            return await _messageRepository.Insert(entity);
        }

        public async Task<List<MessageEntity>> GetAllChatMessages(Guid chatContextId)
        {
            return await _messageRepository.GetAllByContextId(chatContextId);
        }
    }
}
