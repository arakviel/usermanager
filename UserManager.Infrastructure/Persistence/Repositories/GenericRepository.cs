using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UserManager.Core;
using UserManager.Infrastructure.Persistence.Contracts;

namespace UserManager.Infrastructure.Persistence.Repositories;

internal abstract class GenericRepository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.AsNoTracking().FirstAsync(e => e.Id == id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        await _dbSet.Where(e => e.Id == entity.Id)
            .ExecuteUpdateAsync(setters => SettersInit(setters, entity));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Set<T>()
             .Where(e => e.Id == id)
             .ExecuteDeleteAsync();
    }

    protected abstract SetPropertyCalls<T> SettersInit(SetPropertyCalls<T> setters, T entity);
}
