using Application.Room.Request;
using Application.Room.Response;
using MediatR;

namespace Application.Room.CQRS.Commands;

public class CreateRoomCommand : IRequest<RoomResponse>
{
    public RoomRequest RoomRequest { get; set; }
}
