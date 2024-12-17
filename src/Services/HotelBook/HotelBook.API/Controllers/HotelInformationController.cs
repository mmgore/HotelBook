using System.Net;
using HotelBook.Application.Commands.CreateHotelInfo;
using HotelBook.Application.Commands.DeleteHotelInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBook.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelInformationController :ControllerBase
{
    private readonly IMediator _mediator;

    public HotelInformationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [Route("v1/HotelInformation")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateHotelInformation([FromBody]CreateHotelInfoCommand command)
        => Ok(await _mediator.Send(command));
    
    [Route("v1/HotelInformation/{id}")]
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteHotelInformation([FromRoute]Guid id)
        => Ok(await _mediator.Send(new DeleteHotelInfoCommand(id)));
}