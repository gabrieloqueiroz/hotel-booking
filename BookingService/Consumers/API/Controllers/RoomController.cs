using API.Logs;
using Application.Room.DTOs;
using Application.Room.Ports.In;
using Application.Room.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private IRoomManager _roomManager;
    private LogModel _logger;

    public RoomController(IRoomManager roomManager, LogModel logModel)
    {
        _roomManager = roomManager;
        _logger = logModel;
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> Create(RoomDto room)
    {
        var request = new RoomRequest
        {
            RoomDto = room
        };

        var roomCreated = await _roomManager.Create(request);

        if (roomCreated.Success) return Created(" ", roomCreated.Data);

        if(roomCreated.isMappedException()) return BadRequest(roomCreated);

        _logger.RecLog(nameof(Create), $"Response with unknown ErrorCode retuned - {roomCreated}", ELogType.LogInformation);

        return BadRequest(500);
    }
}
