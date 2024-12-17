using System.Linq.Expressions;
using Report.API.Domain.Entities;
using Report.API.Domain.Interfaces;

namespace Report.API.Infrastructure.Repositories;

public class HotelInformationRepository: IHotelInformationRepository
{
    private readonly IRepository<HotelInformation> _repository;

    public HotelInformationRepository(IRepository<HotelInformation> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public async Task InsertAsync(HotelInformation hotelInfo)
    {
        await _repository.InsertAsync(hotelInfo);
    }

    public async Task UpdateAsync(HotelInformation hotelInfo)
    {
        await _repository.UpdateAsync(hotelInfo);
    }

    public async Task DeleteAsync(HotelInformation hotelInfo)
    {
        await _repository.DeleteAsync(hotelInfo);
    }

    public async Task<HotelInformation> GetHotelInformationById(Guid id)
        => await _repository.GetAsync(id);

    public async Task<HotelInformation> GetHotelInformation(Expression<Func<HotelInformation, bool>> predicate)
        => await _repository.GetAsync(predicate);

    public async Task<IEnumerable<HotelInformation>> GetHotelInformations()
        => await _repository.GetAllAsync();
}