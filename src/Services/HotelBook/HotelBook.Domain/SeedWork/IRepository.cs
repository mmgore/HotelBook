using System.Linq.Expressions;

namespace HotelBook.Domain.SeedWork;

public interface IRepository<T> where T : class
{
    IQueryable<T> Queryable();
    IQueryable<T> Queryable(Expression<Func<T, bool>> predicate);
    Task InsertAsync(T entity);
    Task DeleteAsync(Guid id);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
    Task<T> GetAsync(Guid id);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync();
}