using HotelBook.Domain.Exceptions;
using HotelBook.Domain.SeedWork;

namespace HotelBook.Domain.AggregatesModel.HotelAggregate;

public class Hotel : Entity
{
    public string HotelName { get; private set; }
    public string AuthorizedName { get; private set; }
    public string AuthorizedSurname { get; private set; }
    public string FirmTitle { get; private set; }
    public IList<HotelInformation> HotelInformations { get; private set; }
    public Hotel()
    {
        HotelInformations = new List<HotelInformation>();
    }
    public Hotel(string authorizedName, string authorizedSurname, string firmTitle, string hotelName)
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
        HotelName = !string.IsNullOrWhiteSpace(hotelName) ? hotelName : throw new HotelDomainException("Hotel Name cannot be null");
        AuthorizedName = !string.IsNullOrWhiteSpace(authorizedName) ? authorizedName : throw new HotelDomainException("Authorized Name cannot be null");
        AuthorizedSurname = !string.IsNullOrWhiteSpace(authorizedSurname) ? authorizedSurname : throw new HotelDomainException("Authorized Surname cannot be null");;
        FirmTitle = !string.IsNullOrWhiteSpace(firmTitle) ? firmTitle : throw new HotelDomainException("Firm Title cannot be null");;
    }
    public static Hotel Create(string hotelName, string authorizedName, string authorizedSurname, string firmTitle)
    {
        return new Hotel(authorizedName, authorizedSurname, firmTitle, hotelName);
    }

    public void AddHotelInformation(Guid hotelId, string phoneNumber, string email, string location, string note)
    {
        var hotelInformation = new HotelInformation(hotelId, phoneNumber, email, location, note);
        HotelInformations.Add(hotelInformation);
    }
}