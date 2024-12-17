using System.Linq.Expressions;
using Report.API.Domain.Entities;

namespace Report.API.Domain.Interfaces;

public interface IHotelInformationRepository
{
    Task InsertAsync(HotelInformation hotelInfo);
    Task UpdateAsync(HotelInformation hotelInfo);
    Task DeleteAsync(HotelInformation hotelInfo);
    Task<HotelInformation> GetHotelInformationById(Guid id);
    Task<HotelInformation> GetHotelInformation(Expression<Func<HotelInformation, bool>> predicate);
    Task<IEnumerable<HotelInformation>> GetHotelInformations();
}