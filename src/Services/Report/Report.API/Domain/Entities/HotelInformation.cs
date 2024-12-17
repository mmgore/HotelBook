namespace Report.API.Domain.Entities;

public class HotelInformation : Entity
{
    public Guid HotelId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Location { get; set; }
    public string Note { get; set; }
}