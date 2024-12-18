using System.Net;
using Microsoft.AspNetCore.Mvc;
using Report.API.Application.Dtos;
using Report.API.Application.Intefaces;

namespace Report.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportItemController : ControllerBase
{
    private readonly IReportAppService _reportAppService;
    public ReportItemController(IReportAppService reportAppService)
    {
        _reportAppService = reportAppService ?? throw new ArgumentNullException(nameof(reportAppService));
    }

    [Route("v1/Report/{location}")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateHotelLocationReport([FromRoute] string location)
    {
        await _reportAppService.CreateHotelLocationReport(location);
        return Ok();
    }

    [Route("api/v1/ReportList")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ReportListDto>> GetReportList()
        => Ok(await _reportAppService.GetReportList());
}