using HotelBook.Application.Exceptions;
using HotelBook.Domain.AggregatesModel.HotelAggregate;
using HotelBook.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBook.Application.Commands.DeleteHotelInfo;

public class DeleteHotelInfoCommandHandler : IRequestHandler<DeleteHotelInfoCommand, Unit>
{
    private readonly IHotelInformationRepository _hotelInformationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteHotelInfoCommandHandler> _logger;

    public DeleteHotelInfoCommandHandler(IHotelInformationRepository hotelInformationRepository, IUnitOfWork unitOfWork,
        ILogger<DeleteHotelInfoCommandHandler> logger)
    {
        _hotelInformationRepository = hotelInformationRepository ?? throw new ArgumentNullException(nameof(hotelInformationRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(DeleteHotelInfoCommand request, CancellationToken cancellationToken)
    {
        var hotelInfo = await _hotelInformationRepository.GetHotelInformationById(request.Id);
        if (hotelInfo == null)
        {
            _logger.LogError($"The hotel info with id: {request.Id} does not exist");
            throw new NotFoundException(nameof(hotelInfo), request.Id);
        }
        
        await _hotelInformationRepository.DeleteAsync(hotelInfo);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation($"The hotel info with id: {request.Id} has been deleted");
        
        return Unit.Value;
    }
}