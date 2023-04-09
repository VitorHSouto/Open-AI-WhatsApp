using System.Text.Json.Serialization;

namespace VHS_Tarefas.Domain.Messages.OpenAI
{
    public class OpenAIImage
    {
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("n")]
        public int N { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }
    }
}
