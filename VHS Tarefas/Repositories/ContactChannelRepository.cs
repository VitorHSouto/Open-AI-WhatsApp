using Microsoft.EntityFrameworkCore;
using VHS_Tarefas.Data;
using VHS_Tarefas.Entities;

namespace VHS_Tarefas.Repositories
{
    public class ContactChannelRepository : RepositoryBase<ContactChannelEntity>
    {
        public ContactChannelRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<ContactChannelEntity> GetByPhoneNumber(string phoneNumber)
        {
            return await table.FromSqlRaw($"SELECT * FROM contactchannel WHERE phonenumber = '{phoneNumber}'").FirstOrDefaultAsync();
        }
    }
}
