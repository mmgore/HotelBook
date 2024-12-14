using Hotel.Domain.SeedWork;

namespace Hotel.Domain.AggregatesModel.HotelAggregate;

public class HotelInformation : Entity
{
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Location { get; private set; }
    public string Note { get; private set; }
}