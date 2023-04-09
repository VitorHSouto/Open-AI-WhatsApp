using System.ComponentModel.DataAnnotations.Schema;

namespace VHS_Tarefas.Entities
{
    public class ChatContextEntity : IEntityBase
    {
        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }

        [Column("channelid")]
        public Guid ChannelId { get; set; }

        [Column("contactchannelid")]
        public Guid ContactChannelId { get; set; }
        
        [Column("isclosed")]
        public bool IsClosed { get; set; }
    }
}
