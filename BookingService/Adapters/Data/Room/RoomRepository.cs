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

    public Task<RoomEntity> Get(int roomId)
    {
        return _hotelDbContext.Rooms.Where(x => x.Id.Equals(roomId)).FirstAsync();
    }
}
