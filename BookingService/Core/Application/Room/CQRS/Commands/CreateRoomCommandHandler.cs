using Application.Room.DTOs;
using Application.Room.Response;
using Domain.Room.Exceptions;
using Domain.Room.Ports.Out;
using MediatR;

namespace Application.Room.CQRS.Commands;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomResponse>
{

    private IRoomRepository _roomRepository;

    public CreateRoomCommandHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<RoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var roomRequest = request.RoomRequest;

            var entity = RoomDto.MapToEntity(roomRequest.RoomDto);

            await entity.save(_roomRepository);

            roomRequest.RoomDto.Id = entity.Id;

            return new RoomResponse
            {
                Success = true,
                Data = roomRequest.RoomDto
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
}