using Microsoft.EntityFrameworkCore;
using Report.Console.Models;
using System;

namespace Report.Console.Infrastructure;

public class ReportRepository
{
    public async Task UpdateReportAsync(ReportItem reportItem)
    {
        using var context = new ReportContext();
        var reportItemDb = context.ReportItems.FirstOrDefault(x => x.Id == reportItem.Id);
        if (reportItemDb != null)
        {
            reportItemDb.Status = 1;
            await context.SaveChangesAsync();
        }
    }
}