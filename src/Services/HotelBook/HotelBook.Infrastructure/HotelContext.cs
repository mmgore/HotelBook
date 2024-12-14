using HotelBook.Domain.AggregatesModel.HotelAggregate;
using Microsoft.EntityFrameworkCore;

namespace HotelBook.Infrastructure;

public class HotelContext : DbContext
{
    public HotelContext(DbContextOptions<HotelContext> options) : base(options)
    {
    }
    DbSet<Hotel> Hotels { get; set; }
    DbSet<HotelInformation> HotelInformations { get; set; }
    
}