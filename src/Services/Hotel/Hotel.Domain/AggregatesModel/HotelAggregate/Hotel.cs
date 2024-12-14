using Hotel.Domain.SeedWork;

namespace Hotel.Domain.AggregatesModel.HotelAggregate;

public class Hotel : Entity
{
    public string AuthorizedName { get; private set; }
    public string AuthorizedSurname { get; private set; }
    public string FirmTitle { get; private set; }
    public string HotelName { get; private set; }
}