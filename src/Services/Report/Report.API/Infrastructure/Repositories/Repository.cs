using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Report.API.Domain.Entities;
using Report.API.Domain.Interfaces;

namespace Report.API.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly ReportContext _context;
    private DbSet<T> _dbSet;

    public Repository(ReportContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<T>();
    }
    public IQueryable<T> Queryable() => _dbSet;

    public IQueryable<T> Queryable(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);

    public async Task InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetAsync(id);
        _dbSet.Remove(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);
        
        _dbSet.Remove(entity);
        await Task.FromResult(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await Task.FromResult(entity);
    }

    public async Task<T> GetAsync(Guid id) => await _dbSet.FindAsync(id);

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate) => await _dbSet.FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync(); 
}