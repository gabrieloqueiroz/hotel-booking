﻿using Domain.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest;

public class GuestRepository : IGuestRepository
{
    private HotelDbContext _hotelDbContext;

    public GuestRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext;
    }


    public async Task<int> Create(Domain.Entities.Guest guest)
    {
        _hotelDbContext.Guests.Add(guest);
        await _hotelDbContext.SaveChangesAsync();
        return guest.Id;
    }

    public Task<Domain.Entities.Guest?> Get(int guestId)
    {
        return _hotelDbContext.Guests.Where(guest => guest.Id.Equals(guestId)).FirstOrDefaultAsync();
    }
}
