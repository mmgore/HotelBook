using System.Net;
using HotelBook.Application.Commands.CreateHotel;
using HotelBook.Application.Commands.DeleteHotel;
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

    [Route("v1/Hotel")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateHotel([FromBody]CreateHotelCommand command)
        => Ok(await _mediator.Send(command));
    
    [Route("v1/Hotel/{id}")]
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteHotel([FromRoute]Guid id)
        => Ok(await _mediator.Send(new DeleteHotelCommand(id)));
}