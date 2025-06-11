using System.Linq.Expressions;

namespace GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        void Create(T entity);
        Task<int> CreateAsync(T entity);
        void Update(T entity);
        Task<int> UpdateAsync(T entity);
        bool Remove(T entity);
        Task<bool> RemoveAsync(T entity);
        Task<bool> IsExistAsync(Guid id);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T GetById(string code);
        Task<T> GetByIdAsync(string code);
        T GetById(Guid code);
        Task<T> GetByIdAsync(Guid code);

        Task<T> GetByAsync(string type, string value);

        Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
        int Save();
        Task<int> SaveAsync();
    }
}
