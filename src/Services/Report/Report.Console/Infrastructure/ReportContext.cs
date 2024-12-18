using Microsoft.EntityFrameworkCore;
using Report.Console.Models;

namespace Report.Console.Infrastructure;

public class ReportContext : DbContext
{
    public DbSet<ReportItem> ReportItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;User=sa;Password=myStrong1;Database=DataDb;Trusted_Connection=True;Integrated security=False;Application Name=DataDb;TrustServerCertificate=True;");
    }
        
}