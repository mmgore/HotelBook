namespace Report.API.Domain.Entities;

public class ReportItem : Entity
{
    public string Location { get; set; }
    public int HotelCount { get; set; }
    public int PhoneNumberCount { get; set; }
    public ReportStatus Status { get; set; }
}