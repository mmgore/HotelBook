namespace Report.API.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}