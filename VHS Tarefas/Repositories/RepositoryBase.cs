using Microsoft.EntityFrameworkCore;
using VHS_Tarefas.Data;
using VHS_Tarefas.Entities;

namespace VHS_Tarefas.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : IEntityBase
    {
        public readonly ApplicationDbContext _dbContext;
        public DbSet<T> table;
        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
        }

        public async Task<bool> Delete(Guid id) 
        {
            T entity = await table.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return false;

            table.Remove(entity);
            return true;
        }

        public async Task<T?> GetById(Guid id) 
        {
            return await table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> ListAll() 
        {
            var t = await table.ToListAsync();
            return t;
        }

        public async Task<T> Insert(T entity) 
        {
            table.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> Update(T entity) 
        { 
            var updateEntity = await this.GetById(entity.Id);
            if (updateEntity == null)
                throw new Exception("Não é foi possível encontrar a entidade com esse ID.");

            updateEntity = entity;
            table.Update(updateEntity);
            _dbContext.SaveChanges();

            return entity;
        }
    }
}
