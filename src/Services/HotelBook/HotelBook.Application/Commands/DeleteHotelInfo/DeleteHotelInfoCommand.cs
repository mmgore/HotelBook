using MediatR;

namespace HotelBook.Application.Commands.DeleteHotelInfo;

public class DeleteHotelInfoCommand : IRequest
{
    public Guid Id { get; init; }

    public DeleteHotelInfoCommand(Guid id)
    {
        Id = id;
    }
}