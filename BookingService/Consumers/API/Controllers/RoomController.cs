using API.Logs;
using Application.Booking.Response;
using Application.Room.CQRS.Commands;
using Application.Room.CQRS.Queries;
using Application.Room.DTOs;
using Application.Room.Ports.In;
using Application.Room.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private IRoomManager _roomManager;
    private LogModel _logger;
    private IMediator _mediator;

    public RoomController(IRoomManager roomManager, LogModel logModel, IMediator mediator)
    {
        _roomManager = roomManager;
        _logger = logModel;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> Create(RoomDto room)
    {
        var roomCreated = await _mediator.Send(new CreateRoomCommand { RoomRequest = new RoomRequest { RoomDto  = room} });


        /*
         * Chamada sem usar o mediator (CQRS)
         * var roomCreated = await _roomManager.Create(request);
         */


        if (roomCreated.Success) return Created(" ", roomCreated.Data);

        if(roomCreated.isMappedException()) return BadRequest(roomCreated);

        _logger.RecLog(nameof(Create), $"Response with unknown ErrorCode retuned - {roomCreated}", ELogType.LogInformation);

        return BadRequest(500);
    }

    [HttpGet]
    public async Task<ActionResult<RoomDto>> Get(int id)
    {
        var roomResponse = await _mediator.Send(new GetRoomQuery{Id = id});


        /*
         * Chamada sem usar o mediator (CQRS)
         * var roomResponse = await _roomManager.get(id);
         */

        if (!roomResponse.Success) return NotFound(roomResponse);

        return Ok(roomResponse.Data);
    }
}
