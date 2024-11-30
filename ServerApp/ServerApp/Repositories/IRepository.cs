
using System.Linq.Expressions;

namespace ServerApp.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>? filter=null, Expression<Func<T,object>>? include=null);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter);
        Task<T> GetSingleByIdAsync(int id, Expression<Func<T, object>>? include = null);

        
    }
}
