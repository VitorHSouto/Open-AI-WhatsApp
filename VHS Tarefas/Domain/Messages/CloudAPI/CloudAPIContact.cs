using System.Text.Json.Serialization;

namespace VHS_Tarefas.Domain.Messages.CloudAPI
{
    public class CloudAPIContact
    {
        [JsonPropertyName("profile")]
        public CloudAPIContactProfile Profile { get; set; }

        [JsonPropertyName("wa_id")]
        public string WaId { get; set; }
    }

    public class CloudAPIContactProfile
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
