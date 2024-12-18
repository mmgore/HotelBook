using Report.API.Application.Dtos;
using Report.API.Domain.Entities;

namespace Report.API.Application.Intefaces;

public interface IReportAppService
{
    Task CreateHotelLocationReport(string location);
    Task<IEnumerable<ReportListDto>> GetReportList();
    Task<ReportDto> GetReportById(Guid id);
}