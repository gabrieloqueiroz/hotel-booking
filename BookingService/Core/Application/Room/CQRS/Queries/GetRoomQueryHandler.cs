using Application.Room.DTOs;
using Application.Room.Response;
using Domain.Room.Ports.Out;
using MediatR;

namespace Application.Room.CQRS.Queries;

public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, RoomResponse>
{

    private IRoomRepository _roomRepository;

    public GetRoomQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<RoomResponse> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var room = await _roomRepository.get(request.Id);

            if (room == null)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = EErrorCodes.ROOM_NOT_FOUND,
                    Message = "Room not found"
                };
            }

            var roomDto = RoomDto.MapToDto(room);

            return new RoomResponse
            {
                Data = roomDto,
            };
        }
        catch (Exception)
        {
            return new RoomResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.ROOM_NOT_FOUND,
                Message = "Error to found room"
            };
        }
    }
}