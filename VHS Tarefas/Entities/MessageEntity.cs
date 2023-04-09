using System.ComponentModel.DataAnnotations.Schema;
using VHS_Tarefas.Enums;

namespace VHS_Tarefas.Entities
{
    public class MessageEntity : IEntityBase
    {
        [Column("chatcontextid")]
        public Guid ContextId { get; set; }

        [Column("text")]
        public string? Text { get; set; }

        [Column("mediaurl")]
        public string? MediaUrl { get; set; }

        [Column("isuser")]
        public bool IsUser { get; set; }

        [Column("isbot")]
        public bool IsBot { get; set; }
    }
}
