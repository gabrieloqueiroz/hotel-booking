using API.Logs;
using Application.Guest.DTOs;
using Application.Guest.Ports.In;
using Application.Guest.Request;
using Application.Guest.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class GuestController : ControllerBase
{
    private readonly IGuestManager _guestManager;
    private readonly LogModel _logger;

    public GuestController(IGuestManager guestManager, LogModel logModel)
    {
        _guestManager = guestManager;
        _logger = logModel;
    }

    [HttpPost]
    public async Task<ActionResult<GuestResponse>> create(GuestDTO guestRequest)
    {

        var request = new GuestRequest
        {
            Data = guestRequest
        };

        var res = await _guestManager.create(request);

        if (res.Success) return Created(" ", res.Data);

        if (res.isMappedException()) return BadRequest(res);

        _logger.RecLog(nameof(create), $"Response with unknown ErrorCode retuned - {res}", ELogType.LogInformation);

        return BadRequest(500);
    }
}
