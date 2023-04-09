using System.ComponentModel.DataAnnotations.Schema;

namespace VHS_Tarefas.Entities
{
    public class ChannelEntity : IEntityBase
    {
        [Column("phonenumber")]
        public string PhoneNumber { get; set; }
    }
}
