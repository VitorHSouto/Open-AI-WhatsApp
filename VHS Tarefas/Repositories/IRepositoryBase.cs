using VHS_Tarefas.Data;

namespace VHS_Tarefas.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task<List<T>> ListAll();
        public Task<T?> GetById(Guid Id);
        public Task<T?> Insert(T entity);
        public Task<T?> Update(T entity);
        public Task<bool> Delete(Guid id);
    }
}
