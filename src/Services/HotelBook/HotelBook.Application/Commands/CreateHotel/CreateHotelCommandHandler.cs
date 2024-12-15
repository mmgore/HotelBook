using HotelBook.Domain.AggregatesModel.HotelAggregate;
using HotelBook.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBook.Application.Commands.CreateHotel;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Unit>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IHotelInformationRepository _hotelInformationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateHotelCommandHandler> _logger;

    public CreateHotelCommandHandler(IHotelRepository hotelRepository, 
        IHotelInformationRepository hotelInformationRepository, 
        IUnitOfWork unitOfWork,
        ILogger<CreateHotelCommandHandler> logger)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _hotelInformationRepository = hotelInformationRepository ?? throw new ArgumentNullException(nameof(hotelInformationRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = Hotel.Create(request.HotelName, request.AuthorizedName, request.AuthorizedSurname, request.FirmTitle);
        var hotelInformation = HotelInformation.Create(hotel.Id, request.PhoneNumber, request.Email, request.Location,request.Note);
        
        await _hotelRepository.InsertAsync(hotel);
        await _hotelInformationRepository.InsertAsync(hotelInformation);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation($"Created hotel with id {hotel.Id} successfully.");
        
        return Unit.Value;
    }
}