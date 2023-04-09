using System.Text.Json.Serialization;

namespace VHS_Tarefas.Domain.Messages.CloudAPI
{
    public class CloudAPIWebhook
    {
        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("entry")]
        public List<CloudAPIWebhookEntry> Entry { get; set; }
    }

    public class CloudAPIWebhookEntry
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("changes")]
        public List<CloudAPIWebhookChange>? Changes { get; set; }
    }

    public class CloudAPIWebhookValue
    {
        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; }

        [JsonPropertyName("metadata")]
        public CloudAPIWebhookMetadata? Metadata { get; set; }

        [JsonPropertyName("contacts")]
        public List<CloudAPIContact>? Contacts { get; set; }

        [JsonPropertyName("messages")]
        public List<CloudAPIMessage>? Messages { get; set; }
    }

    public class CloudAPIWebhookChange
    {
        [JsonPropertyName("value")]
        public CloudAPIWebhookValue Value { get; set; }

        [JsonPropertyName("field")]
        public string Field { get; set; }
    }
        

    public class CloudAPIWebhookMetadata
    {
        [JsonPropertyName("display_phone_number")]
        public string DisplayPhoneNumber { get; set; }

        [JsonPropertyName("phone_number_id")]
        public string PhoneNumberId { get; set; }
    }
}
