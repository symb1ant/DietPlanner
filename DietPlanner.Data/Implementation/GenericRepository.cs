using DietPlanner.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DietPlanner.Repository.Implementation;
public class GenericRepository<T, TContext> : IRepository<T> where T : class where TContext : DbContext
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

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = _dbSet;

        if (include != null)
        {
            query = include(query);
        }

        return await query.Where(predicate).ToListAsync();
    }

    public async Task<T> GetByIDAsync(long id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var returnVal = await _dbSet.FindAsync(id);

        if (include != null)
        {
            await include(_dbSet).LoadAsync();
        }

        return returnVal;
    }

    public async Task<T> GetByIDAsync(string id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        var returnVal = await _dbSet.FindAsync(id);

        if (include != null)
        {
            await include(_dbSet).LoadAsync();
        }

        return returnVal;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}
