using VHS_Tarefas.Domain.Messages.CloudAPI;
using VHS_Tarefas.Entities;

namespace VHS_Tarefas.Services
{
    public interface IChannelService
    {
        public Task HandleWebhook(CloudAPIWebhook webhook);
        public Task<List<ChannelEntity>> ListAll();
        public Task<ChannelEntity> Save(ChannelEntity entity);
    }
}
