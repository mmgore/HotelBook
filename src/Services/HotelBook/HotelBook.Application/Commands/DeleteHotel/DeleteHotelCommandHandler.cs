using HotelBook.Application.Exceptions;
using HotelBook.Domain.AggregatesModel.HotelAggregate;
using HotelBook.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBook.Application.Commands.DeleteHotel;

public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Unit>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteHotelCommandHandler> _logger;

    public DeleteHotelCommandHandler(IHotelRepository hotelRepository, IUnitOfWork unitOfWork,
        ILogger<DeleteHotelCommandHandler> logger)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetHotelById(request.Id);

        if (hotel == null)
        {
            _logger.LogError($"The hotel with id: {request.Id} does not exist");
            throw new NotFoundException(nameof(Hotel), request.Id);
        }
        
        await _hotelRepository.DeleteAsync(hotel);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation($"Hotel with {request.Id} id was deleted");
        
        return Unit.Value;
    }
}