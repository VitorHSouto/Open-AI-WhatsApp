using System.Text.Json.Serialization;

namespace VHS_Tarefas.Domain.Messages.CloudAPI
{
    public class CloudAPIOutMessage
    {
        public CloudAPIOutMessage() 
        {
            MessagingProduct = "whatsapp";
            RecipientType = "individual";
        }

        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; }

        [JsonPropertyName("recipient_type")]
        public string RecipientType { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("text")]
        public CloudAPIOutMessageText Text { get; set; }

        [JsonPropertyName("image")]
        public CloudAPIImage? Image { get; set; }
    }

    public class CloudAPIOutMessageText
    {
        [JsonPropertyName("preview_url")]
        public bool PreviewUrl { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
