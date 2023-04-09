using System.Text.Json.Serialization;

namespace VHS_Tarefas.Domain.Messages.CloudAPI
{
    public class CloudAPIOutMessageResponse
    {
        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; }

        [JsonPropertyName("contacts")]
        public List<CloudAPIContact> Contacts { get; set; }

        [JsonPropertyName("messages")]
        public List<CloudAPIMessageResponse> Messages { get; set; }
    }
    

    public class CloudAPIMessageResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
