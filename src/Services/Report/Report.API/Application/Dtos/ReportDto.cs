namespace Report.API.Application.Dtos;

public class ReportDto
{
    public string ReportName { get; set; }
    public string Location { get; set; }
    public int HotelCount { get; set; }
    public int PhoneNumberCount { get; set; }
    public Status Status { get; set; }
}