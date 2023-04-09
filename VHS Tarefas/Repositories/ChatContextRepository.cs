using Microsoft.EntityFrameworkCore;
using Npgsql;
using VHS_Tarefas.Data;
using VHS_Tarefas.Entities;
using static Dapper.SqlMapper;

namespace VHS_Tarefas.Repositories
{
    public class ChatContextRepository : RepositoryBase<ChatContextEntity>
    {
        public ChatContextRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<ChatContextEntity> GetLastActive(Guid contactChannelId)
        {
            return await table.FromSqlRaw($"SELECT * FROM chatcontext WHERE contactchannelid = '{contactChannelId}' AND isclosed = false ORDER BY createdat ASC LIMIT 1").FirstOrDefaultAsync();
        }

        public async Task CloseLastChat(Guid contactChannelId, Guid channelId)
        {
            var updateEntity = await GetLastActive(contactChannelId);
            if (updateEntity == null)
                throw new Exception("Não é foi possível encontrar a entidade com esse ID.");

            updateEntity.UpdatedAt = DateTime.Now.ToUniversalTime();
            updateEntity.IsClosed = true;

            _dbContext.chatcontext.Update(updateEntity);
            _dbContext.SaveChanges();
        }
    }
}
