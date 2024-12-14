using HotelBook.Domain.Exceptions;
using HotelBook.Domain.SeedWork;

namespace HotelBook.Domain.AggregatesModel.HotelAggregate;

public class HotelInformation : Entity
{
    public Guid HotelId { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Location { get; private set; }
    public string Note { get; private set; }
    public HotelInformation(Guid hotelId, string phoneNumber, string email, string location, string note)
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
        HotelId = hotelId;
        PhoneNumber = !string.IsNullOrWhiteSpace(phoneNumber) ? phoneNumber : throw new HotelDomainException(nameof(phoneNumber));
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new HotelDomainException(nameof(email));
        Location = !string.IsNullOrWhiteSpace(location) ? location : throw new HotelDomainException(nameof(location));
        Note = !string.IsNullOrWhiteSpace(note) ? note : throw new HotelDomainException(nameof(note));
    }

    public static HotelInformation Create(Guid hotelId, string phoneNumber, string email, string location, string note)
    {
        return new HotelInformation(hotelId, phoneNumber, email, location, note);
    }
}