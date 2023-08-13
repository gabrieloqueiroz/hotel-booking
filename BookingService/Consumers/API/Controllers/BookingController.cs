using API.Logs;
using Application.Booking.DTOs;
using Application.Booking.Ports.In;
using Application.Booking.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingManager _bookingManager;
    private LogModel _logger;

    public BookingController(IBookingManager bookingManager, LogModel logger)
    {
        _bookingManager = bookingManager;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<BookingDto>> Create(BookingDto bookingDto)
    {
        var bookingRequest = new BookingRequest
        {
            data = bookingDto
        };

        var bookingCreated = await _bookingManager.createBooking(bookingRequest);

        if (bookingCreated.Success) return Created(" ", bookingCreated.data);

        if (bookingCreated.isMappedException()) return BadRequest(bookingCreated);

        _logger.RecLog(nameof(Create), $"Response with unknown ErrorCode retuned - {bookingCreated}", ELogType.LogInformation);

        return BadRequest(500);
    }
}