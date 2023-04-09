using System.Text.Json.Serialization;

namespace VHS_Tarefas.Domain.Messages.OpenAI
{
    public class OpenAIImageOut
    {
        [JsonPropertyName("created")]
        public int Created { get; set; }

        [JsonPropertyName("data")]
        public List<OpenAIImageData> Data { get; set; }
    }

    public class OpenAIImageData
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
