using HotelBook.Application.Exceptions;
using HotelBook.Domain.AggregatesModel.HotelAggregate;
using HotelBook.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBook.Application.Commands.CreateHotelInfo;

public class CreateHotelInfoCommandHandler : IRequestHandler<CreateHotelInfoCommand, Unit>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IHotelInformationRepository _hotelInformationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateHotelInfoCommandHandler> _logger;

    public CreateHotelInfoCommandHandler(IHotelRepository hotelRepository,
        IHotelInformationRepository hotelInformationRepository, IUnitOfWork unitOfWork,
        ILogger<CreateHotelInfoCommandHandler> logger)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _hotelInformationRepository = hotelInformationRepository ?? throw new ArgumentNullException(nameof(hotelInformationRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(CreateHotelInfoCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetHotelById(request.HotelId);
        if (hotel == null)
        {
            _logger.LogError($"Hotel with id: {request.HotelId} does not exist");
            throw new NotFoundException(nameof(hotel), request.HotelId);
        }

        var hotelInformation = HotelInformation.Create(request.HotelId, request.PhoneNumber, request.Email, request.Location, request.Note);
        await _hotelInformationRepository.InsertAsync(hotelInformation);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation($"Hotel with id: {request.HotelId} has been created");
        
        return Unit.Value;
    }
}