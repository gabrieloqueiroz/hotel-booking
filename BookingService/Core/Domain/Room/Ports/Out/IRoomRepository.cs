using Domain.Room.Entities;

namespace Domain.Room.Ports.Out;

public interface IRoomRepository
{
    Task<RoomEntity> Get(int roomId);
    Task<int> Create(RoomEntity room);
}
