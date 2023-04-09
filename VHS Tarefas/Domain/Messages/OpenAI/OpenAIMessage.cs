using System.Text.Json.Serialization;

namespace VHS_Tarefas.Domain.Messages.OpenAI
{
    public class OpenAIMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
