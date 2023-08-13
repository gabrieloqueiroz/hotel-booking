using Application.Room.Request;
using Application.Room.Response;

namespace Application.Room.Ports.In;

public interface IRoomManager
{
    Task<RoomResponse> Create(RoomRequest request);
    Task<RoomResponse> get(int id);
}
