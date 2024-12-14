using HotelBook.Domain.AggregatesModel.HotelAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBook.Infrastructure.EntityConfigrations;

public class HotelEntityTypeConfigration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(b=>b.Id);
        
        builder.Property(b=> b.HotelName)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(b=> b.AuthorizedName)
            .HasMaxLength(25)
            .IsRequired();
        
        builder.Property(b => b.AuthorizedSurname)
            .HasMaxLength(25)
            .IsRequired();
        
        builder.Property(b => b.FirmTitle)
            .HasMaxLength(100)
            .IsRequired();
    }
}