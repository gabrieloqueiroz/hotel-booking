﻿using Application.Booking.Ports.In;
using Application.Booking.Response;
using MediatR;

namespace Application.Booking.CQRS.Queries;

public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingResponse>
{
    private IBookingManager _bookingManager;

    public GetBookingQueryHandler(IBookingManager bookingManager)
    {
        _bookingManager = bookingManager;
    }

    public Task<BookingResponse> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        return _bookingManager.GetBooking(request.Id);
    }
}
