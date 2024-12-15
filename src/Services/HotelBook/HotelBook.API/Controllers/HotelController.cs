using System.Net;
using HotelBook.Application.Commands.CreateHotel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBook.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private readonly IMediator _mediator;

    public HotelController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Route("api/v1/Hotels")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateHotel([FromBody]CreateHotelCommand command)
        => Ok(await _mediator.Send(command));
}