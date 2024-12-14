using System.Linq.Expressions;

namespace Hotel.Domain.AggregatesModel.HotelAggregate;

public interface IHotelInformationRepository
{
    Task InsertAsync(HotelInformation hotelInfo);
    Task UpdateAsync(HotelInformation hotelInfo);
    Task DeleteAsync(HotelInformation hotelInfo);
    Task<HotelInformation> GetContactInformationById(Guid id);
    Task<HotelInformation> GetContactInformation(Expression<Func<HotelInformation, bool>> predicate);
    Task<IEnumerable<HotelInformation>> GetContactInformations();
}