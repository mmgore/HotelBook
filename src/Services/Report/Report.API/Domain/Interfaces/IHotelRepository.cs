using System.Linq.Expressions;
using Report.API.Domain.Entities;

namespace Report.API.Domain.Interfaces;

public interface IHotelRepository
{
    Task InsertAsync(Hotel hotel);
    Task UpdateAsync(Hotel hotel);
    Task DeleteAsync(Hotel hotel);
    Task<Hotel> GetHotelById(Guid id);
    Task<Hotel> GetHotel(Expression<Func<Hotel, bool>> predicate);
    Task<IEnumerable<Hotel>> GetHotels();
    Task<Hotel> GetHotelsWithInfos(Guid id);
}