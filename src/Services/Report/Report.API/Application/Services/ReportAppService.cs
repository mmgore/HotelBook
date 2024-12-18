using Report.API.Application.Intefaces;
using Report.API.Domain.Entities;
using Report.API.Domain.Interfaces;

namespace Report.API.Application.Services;

public class ReportAppService : IReportAppService
{
    private readonly IReportRepository _reportRepository;
    private readonly IHotelInformationRepository _hotelInformationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageSender _messageSender;
    public ReportAppService(IReportRepository reportRepository, IHotelInformationRepository hotelInformationRepository, 
        IUnitOfWork unitOfWork, IMessageSender messageSender)
    {
        _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        _hotelInformationRepository = hotelInformationRepository ?? throw new ArgumentNullException(nameof(hotelInformationRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
    }

    public async Task CreateHotelLocationReport(string location)
    {
        var hotellocationReport = await _hotelInformationRepository.GetHotelLocationReport(location);

        var reportItem = new ReportItem()
        {
            Id = Guid.NewGuid(),
            Location = hotellocationReport.Location,
            HotelCount = hotellocationReport.HotelCount,
            PhoneNumberCount = hotellocationReport.PhoneNumberCount,
            ReportName = $"Report {location}",
            Status = 0,
            CreatedDate = DateTime.Now
        };
        
        await _reportRepository.InsertAsync(reportItem);
        await _unitOfWork.SaveChangesAsync();
        
        _messageSender.PublishMessage(reportItem);
    }
}