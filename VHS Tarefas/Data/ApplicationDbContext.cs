using Microsoft.EntityFrameworkCore;
using VHS_Tarefas.Entities;

namespace VHS_Tarefas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ChannelEntity> channel { get; set; }
        public DbSet<MessageEntity> message { get; set; }
        public DbSet<ChatContextEntity> chatcontext { get; set; }
        public DbSet<ContactChannelEntity> contactchannel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
    }
}
