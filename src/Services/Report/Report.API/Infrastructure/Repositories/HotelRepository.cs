using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Report.API.Domain.Entities;
using Report.API.Domain.Interfaces;

namespace Report.API.Infrastructure.Repositories;

public class HotelRepository: IHotelRepository
{
    private readonly IRepository<Hotel> _repository;

    public HotelRepository(IRepository<Hotel> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public async Task InsertAsync(Hotel hotel)
    {
        await _repository.InsertAsync(hotel);
    }

    public async Task UpdateAsync(Hotel hotel)
    {
        await _repository.UpdateAsync(hotel);
    }

    public async Task DeleteAsync(Hotel hotel)
    {
        await _repository.DeleteAsync(hotel);
    }

    public async Task<Hotel> GetHotelById(Guid id)
        => await _repository.GetAsync(id);

    public async Task<Hotel> GetHotel(Expression<Func<Hotel, bool>> predicate)
        => await _repository.GetAsync(predicate);

    public async Task<IEnumerable<Hotel>> GetHotels()
        => await _repository.GetAllAsync();

    public async Task<Hotel> GetHotelsWithInfos(Guid id)
        => await _repository.Queryable(q => q.Id == id).Include(c => c.HotelInformations).FirstOrDefaultAsync();

}