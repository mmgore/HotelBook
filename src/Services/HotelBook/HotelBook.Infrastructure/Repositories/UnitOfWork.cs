using HotelBook.Domain.SeedWork;

namespace HotelBook.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private bool _disposed = false;
    private readonly HotelContext _context;

    public UnitOfWork(HotelContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}