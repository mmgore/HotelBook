using MediatR;

namespace HotelBook.Application.Commands.DeleteHotel;

public class DeleteHotelCommand : IRequest
{
    public Guid Id { get; init; }

    public DeleteHotelCommand(Guid id)
    {
        Id = id;
    }
}