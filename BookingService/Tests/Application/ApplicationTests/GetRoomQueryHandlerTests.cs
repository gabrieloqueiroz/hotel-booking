using Application.Room.CQRS.Queries;
using Domain.Room.Entities;
using Domain.Room.Ports.Out;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

public class GetRoomQueryHandlerTests
{
    [Test]
    public async Task Should_Return_Room()
    {
        // Given
        var query = new GetRoomQuery { Id = 1 };

        var repoMock = new Mock<IRoomRepository>();
        var fakeRoom = new RoomEntity() { Id = 1 };
        repoMock.Setup(x => x.get(query.Id)).Returns(Task.FromResult(fakeRoom));

        // When
        var handler = new GetRoomQueryHandler(repoMock.Object);
        var res = await handler.Handle(query, CancellationToken.None);
        
        // Then
        Assert.True(res.Success);
        Assert.NotNull(res.Data);
    }

    [Test]
    public async Task Should_Return_ProperError_Message_WhenRoom_NotFound()
    {
        // Given
        var query = new GetRoomQuery { Id = 1 };
        var repoMock = new Mock<IRoomRepository>();

        // When
        var handler = new GetRoomQueryHandler(repoMock.Object);
        var res = await handler.Handle(query, CancellationToken.None);

        // Then
        Assert.False(res.Success);
        Assert.AreEqual(res.ErrorCode, Application.EErrorCodes.ROOM_NOT_FOUND);
        Assert.AreEqual(res.Message, "Could not find a Room with the given Id");
    }
}
