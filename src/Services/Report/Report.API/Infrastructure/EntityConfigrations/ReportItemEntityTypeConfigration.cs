using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Report.API.Domain.Entities;

namespace Report.API.Infrastructure.EntityConfigrations;

public class ReportItemEntityTypeConfigration : IEntityTypeConfiguration<ReportItem>
{
    public void Configure(EntityTypeBuilder<ReportItem> builder)
    {
        builder.HasKey(b=>b.Id);
        
        builder.Property(b => b.ReportName)
            .IsRequired();
        
        builder.Property(b => b.Location)
            .IsRequired();
        
        builder.Property(b=> b.PhoneNumberCount)
            .IsRequired();

        builder.Property(b => b.HotelCount)
            .IsRequired();
        
        builder.Property(b=> b.Status)
            .IsRequired();
    }
}