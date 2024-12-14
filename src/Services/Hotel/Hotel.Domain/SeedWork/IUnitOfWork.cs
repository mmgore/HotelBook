namespace Hotel.Domain.SeedWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}