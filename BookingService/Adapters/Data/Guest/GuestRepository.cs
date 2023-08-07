using Domain.Guest.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest;

public class GuestRepository : IGuestRepository
{
    private HotelDbContext _hotelDbContext;

    public GuestRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext;
    }


    public async Task<int> Create(Domain.Guest.Entities.GuestEntity guest)
    {
        _hotelDbContext.Guests.Add(guest);
        await _hotelDbContext.SaveChangesAsync();
        return guest.Id;
    }

    public Task<Domain.Guest.Entities.GuestEntity?> Get(int guestId)
    {
        return _hotelDbContext.Guests.Where(guest => guest.Id.Equals(guestId)).FirstOrDefaultAsync();
    }
}
