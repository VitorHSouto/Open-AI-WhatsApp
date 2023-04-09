using System.Text.Json.Serialization;

namespace VHS_Tarefas.Domain.Messages.CloudAPI
{
    public class CloudAPIMessage
    {
        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("text")]
        public CloudAPIMessageText? Text { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("image")]
        public CloudAPIImage? Image { get; set; }
    }

    public class CloudAPIMessageText
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class CloudAPIImage
    {
        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}
