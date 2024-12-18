using Report.API.Domain.Entities;
using Report.API.Application.Dtos;

public static class StatusMapper
{
    // Map ReportStatus to Status
    public static Status ToDtoStatus(this ReportStatus reportStatus)
    {
        return reportStatus switch
        {
            ReportStatus.Preparing => Status.Preparing,
            ReportStatus.Completed => Status.Completed,
            _ => throw new ArgumentException($"Unknown ReportStatus: {reportStatus}")
        };
    }

    // Map Status to ReportStatus
    public static ReportStatus ToReportStatus(this Status status)
    {
        return status switch
        {
            Status.Preparing => ReportStatus.Preparing,
            Status.Completed => ReportStatus.Completed,
            _ => throw new ArgumentException($"Unknown Status: {status}")
        };
    }
}