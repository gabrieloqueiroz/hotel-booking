using Application.Booking.Request;
using Application.Booking.Response;
using MediatR;

namespace Application.Booking.CQRS.Commands;

public class CreateBookingCommand : IRequest<BookingResponse>
{
    public BookingRequest BookingRequest { get; set; }
}