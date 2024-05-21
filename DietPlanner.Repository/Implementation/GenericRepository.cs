using DietPlanner.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Repository.Implementation;
public class GenericRepository<T, TContext> : IRepository<T> where T : class where TContext: DbContext
{
    private readonly TContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(TContext dbContext)
    {
        _context = dbContext;
        _dbSet = _context.Set<T>();
    }
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIDAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}
