using Domain.Booking.Entities;
using Domain.Booking.Ports.Out;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Booking;

public class BookingRepository : IBookingRepository
{
    public readonly HotelDbContext _hotelDBContext;

    public BookingRepository(HotelDbContext hotelDbContext) 
    {
        _hotelDBContext= hotelDbContext;
    }

    public async Task<int> CreateBooking(BookingEntity booking)
    {
        _hotelDBContext.Bookings.Add(booking);
        await _hotelDBContext.SaveChangesAsync();
        return booking.Id;
    }

    public Task<BookingEntity> GetBooking(int id)
    {
        return _hotelDBContext.Bookings.Where(x => x.Id.Equals(id)).FirstAsync();
    }
}
