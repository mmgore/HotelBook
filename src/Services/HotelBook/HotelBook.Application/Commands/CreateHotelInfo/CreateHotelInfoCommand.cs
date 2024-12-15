using MediatR;

namespace HotelBook.Application.Commands.CreateHotelInfo;

public class CreateHotelInfoCommand : IRequest
{
    public Guid HotelId { get; init; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Location { get; set; }
    public string Note { get; set; }
}