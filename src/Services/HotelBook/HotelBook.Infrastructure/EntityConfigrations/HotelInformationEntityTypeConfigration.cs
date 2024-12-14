using HotelBook.Domain.AggregatesModel.HotelAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBook.Infrastructure.EntityConfigrations;

public class HotelInformationEntityTypeConfigration : IEntityTypeConfiguration<HotelInformation>
{
    public void Configure(EntityTypeBuilder<HotelInformation> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(b => b.Location)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.Note)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.HasOne<Hotel>()
            .WithMany(b => b.HotelInformations)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(b => b.HotelId); 
    }
}