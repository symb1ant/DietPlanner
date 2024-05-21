
using System.Linq.Expressions;

namespace DietPlanner.Repositories.Interfaces;
public interface IRepository<T> where T : class
{
    Task<T> GetByIDAsync(long id);
    Task<T> GetByIDAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}