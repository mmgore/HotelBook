using MediatR;

namespace HotelBook.Application.Commands.CreateHotel;

public class CreateHotelCommand : IRequest
{
    public string HotelName { get; init; }
    public string AuthorizedName { get; init; }
    public string AuthorizedSurname { get; init; }
    public string FirmTitle { get; init; }
    public string PhoneNumber { get; init; }
    public string Email { get; init; }
    public string Location { get; init; }
    public string Note { get; init; }
}