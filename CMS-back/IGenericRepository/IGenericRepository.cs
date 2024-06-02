using System.Linq.Expressions;

namespace CMS_back.IGenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(string id, params string[] includeProperties);
        Task<IEnumerable<T>> GetAllAsync(params string[] includeProperties);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, params string[] includeProperties);
        Task<T> FindFirstAsync(Expression<Func<T, bool>> expression , params string[] includeProperties);
    }
}
