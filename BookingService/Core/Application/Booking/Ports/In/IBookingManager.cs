using Application.Booking.DTOs;
using Application.Booking.Request;
using Application.Booking.Response;
using Application.Payment.Response;

namespace Application.Booking.Ports.In;

public interface IBookingManager
{
    Task<BookingResponse> createBooking(BookingRequest bookingRequest);
    Task<BookingResponse> GetBooking(int id);
    Task<PaymentResponse> PaymentForABooking(PaymentRequestDto paymentRequestDto);
}
