using Microsoft.EntityFrameworkCore;
using Report.API.Domain.Entities;

namespace Report.API.Infrastructure;

public class ReportContext : DbContext
{
   
        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelInformation> HotelInformations { get; set; }
        public DbSet<ReportItem> ReportItems { get; set; }
}