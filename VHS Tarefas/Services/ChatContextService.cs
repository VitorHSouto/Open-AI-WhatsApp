using VHS_Tarefas.Entities;
using VHS_Tarefas.Repositories;

namespace VHS_Tarefas.Services
{
    public class ChatContextService
    {
        private ChatContextRepository _chatContextRepository;
        public ChatContextService(
            ChatContextRepository chatContextRepository
            )
        {
            _chatContextRepository = chatContextRepository;
        }

        public async Task<ChatContextEntity> GetCurrentContext(Guid channelId, Guid contactChannelId)
        {
            var context = await _chatContextRepository.GetLastActive(contactChannelId);
            if (context != null)
                return context;

            context = new ChatContextEntity();
            context.ChannelId = channelId;
            context.ContactChannelId = contactChannelId;
            context.CreatedAt = DateTime.Now.ToUniversalTime();
            context.UpdatedAt = DateTime.Now.ToUniversalTime();

            return await _chatContextRepository.Insert(context);
        }

        public async Task CloseLastContext(Guid channelId, Guid contactChannelId)
        {
            await _chatContextRepository.CloseLastChat(contactChannelId, channelId);
        }
    }
}
