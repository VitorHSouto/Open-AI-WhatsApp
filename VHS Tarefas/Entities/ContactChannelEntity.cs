using System.ComponentModel.DataAnnotations.Schema;

namespace VHS_Tarefas.Entities
{
    public class ContactChannelEntity : IEntityBase
    {
        [Column("name")]
        public string? Name { get; set; }
        [Column("phonenumber")]
        public string? PhoneNumber { get; set; }
    }
}
