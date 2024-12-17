using Report.API.Domain.Entities;

namespace Report.API.Domain.Interfaces;

public interface IReportRepository
{
    Task InsertAsync(ReportItem reportItem);
}