
namespace DietPlanner.Repositories.Interfaces;
public interface IRepository<T> where T : class
{
    Task<T> GetByIDAsync(long id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
