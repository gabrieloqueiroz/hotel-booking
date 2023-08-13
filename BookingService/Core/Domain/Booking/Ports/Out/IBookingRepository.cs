using Domain.Booking.Entities;

namespace Domain.Booking.Ports.Out;

public interface IBookingRepository
{
    Task<BookingEntity> GetBooking(int id);

    Task<int> CreateBooking(BookingEntity booking);
}
