using Microsoft.EntityFrameworkCore;
using VHS_Tarefas.Data;
using VHS_Tarefas.Entities;

namespace VHS_Tarefas.Repositories
{
    public class ChannelRepository : RepositoryBase<ChannelEntity>
    {
        public ChannelRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<ChannelEntity> GetByPhoneNumber(string phonenumber)
        {
            return await table.FromSqlRaw($"SELECT * FROM channel WHERE phonenumber = '{phonenumber}'").FirstOrDefaultAsync();
        }
    }
}
