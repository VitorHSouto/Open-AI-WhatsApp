using Microsoft.EntityFrameworkCore;
using VHS_Tarefas.Data;
using VHS_Tarefas.Entities;

namespace VHS_Tarefas.Repositories
{
    public class MessageRepository : RepositoryBase<MessageEntity>
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<MessageEntity>> GetAllByContextId(Guid chatContextId)
        {
            return await table.FromSqlRaw($"SELECT * FROM message WHERE chatcontextid = '{chatContextId}' ORDER BY createdat").ToListAsync();
        }
    }
}
