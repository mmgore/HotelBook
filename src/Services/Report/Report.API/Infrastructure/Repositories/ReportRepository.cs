using Report.API.Domain.Entities;
using Report.API.Domain.Interfaces;

namespace Report.API.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly IRepository<ReportItem> _repository;

    public ReportRepository(IRepository<ReportItem> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task InsertAsync(ReportItem reportItem)
    {
        await _repository.InsertAsync(reportItem);
    }

    public async Task<IEnumerable<ReportItem>> GetReportItemsAsync()
        => await _repository.GetAllAsync();
}