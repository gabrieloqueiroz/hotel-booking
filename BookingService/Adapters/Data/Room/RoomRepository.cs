using Domain.Room.Entities;
using Domain.Room.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Data.Room;

public class RoomRepository : IRoomRepository
{

    private readonly HotelDbContext _hotelDbContext;

    public RoomRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext;
    }

    public async Task<int> Create(RoomEntity room)
    {
       _hotelDbContext.Rooms.Add(room);
        await _hotelDbContext.SaveChangesAsync();
        return room.Id;
    }

    public Task<RoomEntity> get(int roomId)
    {
        return _hotelDbContext.Rooms
            .Where(g => g.Id.Equals(roomId)).FirstAsync();
    }

    public Task<RoomEntity> getAggregate(int roomId)
    {
        return _hotelDbContext.Rooms
            .Include(room => room.Bookings)
            .Where(g => g.Id.Equals(roomId)).FirstAsync();
    }
}
