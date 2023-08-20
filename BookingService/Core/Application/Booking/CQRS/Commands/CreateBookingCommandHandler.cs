using Application.Booking.Ports.In;
using Application.Booking.Response;
using MediatR;

namespace Application.Booking.CQRS.Commands;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
{
    private IBookingManager _bookingManager;

    public CreateBookingCommandHandler(IBookingManager bookingManager)
    {
        _bookingManager = bookingManager;
    }

    public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        return await _bookingManager.createBooking(request.BookingRequest);
    }
}