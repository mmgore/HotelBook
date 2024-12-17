namespace Report.API.Domain.Entities;

public class Hotel : Entity
{
    public string HotelName { get; set; }
    public string AuthorizedName { get; set; }
    public string AuthorizedSurname { get; set; }
    public string FirmTitle { get; set; }
    public IList<HotelInformation> HotelInformations { get; set; } = new List<HotelInformation>();
}