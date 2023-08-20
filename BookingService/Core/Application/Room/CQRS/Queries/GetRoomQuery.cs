using Application.Room.Response;
using MediatR;

namespace Application.Room.CQRS.Queries;

public class GetRoomQuery : IRequest<RoomResponse>
{
    public int Id { get; set; }
}

