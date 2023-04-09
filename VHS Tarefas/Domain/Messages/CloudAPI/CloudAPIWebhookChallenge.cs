using System.Text.Json.Serialization;

namespace VHS_Tarefas.Domain.Messages.CloudAPI
{
    public class CloudAPIWebhookChallenge
    {
        [JsonPropertyName("challenge")]
        public string Challenge { get; set; }
    }
}
