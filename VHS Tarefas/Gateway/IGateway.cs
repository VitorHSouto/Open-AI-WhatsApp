using VHS_Tarefas.Domain.Messages.CloudAPI;

namespace VHS_Tarefas.Gateway
{
    public interface IGateway
    {
        public Task SendMessage(string message);
        public Task SendImage(string imageUrl);
    }
}
