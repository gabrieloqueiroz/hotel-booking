using Application.Room.DTOs;
using Application.Room.Ports.In;
using Application.Room.Request;
using Application.Room.Response;
using Domain.Room.Exceptions;

namespace Application.Room;

public class RoomManager : IRoomManager
{
    private Domain.Room.Ports.Out.IRoomRepository _roomRepository;

    public RoomManager(Domain.Room.Ports.Out.IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<RoomResponse> Create(RoomRequest requestDto)
    {
        try
        {
            var entity = RoomDto.MapToEntity(requestDto.RoomDto);

            await entity.save(_roomRepository);

            requestDto.RoomDto.Id = entity.Id;

            return new RoomResponse
            {
                Success = true,
                Data = requestDto.RoomDto
            };
        }
        catch (MissingRequiredRoomInformation)
        {
            return new RoomResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.ROOM_MISSING_REQUIRED_INFO,
                Message = "Missing required information passed"
            };
        }
    }

    public Task<RoomResponse> get(int id)
    {
        throw new NotImplementedException();
    }
}
