
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DietPlanner.Data.Interfaces;
public interface IRepository<T> where T : class
{
    Task<T> GetByIDAsync(long id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task<T> GetByIDAsync(string id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
