using Report.API.Domain.Entities;

namespace Report.API.Application.Intefaces;

public interface IReportAppService
{
    Task InsertReportAsync(string location);
}