using Application.Booking.Response;
using MediatR;

namespace Application.Booking.CQRS.Query;

public class GetBookingQuery : IRequest<BookingResponse>
{
    public int Id { get; set; }
}
