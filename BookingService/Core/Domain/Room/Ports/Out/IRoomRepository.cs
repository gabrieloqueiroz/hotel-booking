using Domain.Room.Entities;

namespace Domain.Room.Ports.Out;

public interface IRoomRepository
{
    Task<RoomEntity> get(int roomId);
    Task<int> Create(RoomEntity room);
    Task<RoomEntity> getAggregate(int roomId);
}
