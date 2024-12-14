namespace HotelBook.Domain.SeedWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}