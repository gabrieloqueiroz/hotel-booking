using Application.Booking.Request;
using Application.Booking.Response;

namespace Application.Booking.Ports.In;

public interface IBookingManager
{
    Task<BookingResponse> createBooking(BookingRequest bookingRequest);
    Task<BookingResponse> getBooking(int id);
}
