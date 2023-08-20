using API.Logs;
using Application.Booking.Commands;
using Application.Booking.DTOs;
using Application.Booking.Ports.In;
using Application.Booking.Request;
using Application.Booking.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingManager _bookingManager;
    private readonly LogModel _logger;
    private readonly IMediator _mediator;

    public BookingController(IBookingManager bookingManager, LogModel logger, IMediator mediator)
    {
        _bookingManager = bookingManager;
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("/create")]
    public async Task<ActionResult<BookingDto>> Create(BookingDto bookingDto)
    {
        var bookingRequest = new BookingRequest
        {
            data = bookingDto
        };

        var bookingCreated = await _mediator.Send (new CreateBookingCommand { BookingRequest = bookingRequest });

        if (bookingCreated.Success) return Created(" ", bookingCreated.data);

        if (bookingCreated.isMappedException()) return BadRequest(bookingCreated);

        _logger.RecLog(nameof(Create), $"Response with unknown ErrorCode retuned - {bookingCreated}", ELogType.LogInformation);

        return BadRequest(500);
    }

    [HttpPost("{bookingId}/payment")]
    public async Task<ActionResult<BookingResponse>> Payment(PaymentRequestDto paymentRequestDto, int bookingId)
    {
        paymentRequestDto.BookingId = bookingId;
        var res = await _bookingManager.PaymentForABooking(paymentRequestDto);

        if (res.Success) return Ok(res.data);

        return BadRequest(res);
    }
}