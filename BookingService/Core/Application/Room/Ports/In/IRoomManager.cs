using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room.Ports.In;

public interface IRoomManager
{
    Task<RoomResponse> Create(CreateRoomRequest request);
    Task<RoomResponse> Get(int id);
}
