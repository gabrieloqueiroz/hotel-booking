using Application.Booking.Response;
using MediatR;

namespace Application.Booking.CQRS.Queries;

public class GetBookingQuery : IRequest<BookingResponse>
{
    public int Id { get; set; }
}
