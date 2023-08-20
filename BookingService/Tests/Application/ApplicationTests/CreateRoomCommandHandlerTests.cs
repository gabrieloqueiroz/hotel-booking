using Application.Room.CQRS.Commands;
using Application.Room.DTOs;
using Application.Room.Request;
using Domain.Room.Entities;
using Domain.Room.Ports.Out;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

public class CreateRoomCommandHandlerTests
{
    [Test]
    public async Task Should_Not_CreateRoom_If_Name_IsNot_Provided()
    {
        // Given
        var roomDto = new RoomDto()
        {
            InMaintenance = false,
            Level = 1,
            Currency = Domain.Guest.Enums.EAcceptedCurrencies.Dollar,
            Name = "",
            Price = 100
        };

        var command = new CreateRoomCommand()
        {
            RoomRequest = new RoomRequest
            {
                RoomDto = roomDto
            }
        };

        var repoMock = new Mock<IRoomRepository>();
        repoMock.Setup(x => x.Create(It.IsAny<RoomEntity>()))
            .Returns(Task.FromResult(1));

        // When
        var handler = new CreateRoomCommandHandler(repoMock.Object);
        var res = await handler.Handle(command, CancellationToken.None);

        // Then
        Assert.NotNull(res);
        Assert.False(res.Success);
        Assert.AreEqual(res.ErrorCode, Application.EErrorCodes.ROOM_MISSING_REQUIRED_INFO);
        Assert.AreEqual(res.Message, "Missing required information passed");
    }

    [Test]
    public async Task Should_Not_CreateRoom_If_Price_Is_Invalid()
    {
        // Given
        var roomDto = new RoomDto()
        {
            InMaintenance = false,
            Level = 1,
            Currency = Domain.Guest.Enums.EAcceptedCurrencies.Dollar,
            Name = "Room test",
            Price = 9
        };

        var command = new CreateRoomCommand()
        {
           RoomRequest = new RoomRequest { RoomDto = roomDto }
        };

        var repoMock = new Mock<IRoomRepository>();
        repoMock.Setup(x => x.Create(It.IsAny<RoomEntity>()))
            .Returns(Task.FromResult(1));

        // When
        var handler = new CreateRoomCommandHandler(repoMock.Object);
        var res = await handler.Handle(command, CancellationToken.None);

        // Then
        Assert.NotNull(res);
        Assert.False(res.Success);
        Assert.AreEqual(res.ErrorCode, Application.EErrorCodes.ROOM_MISSING_REQUIRED_INFO);
        Assert.AreEqual(res.Message, "Room price is invalid");
    }


    [Test]
    public async Task Should_CreateRoom()
    {
        // Given
        var roomDto = new RoomDto()
        {
            InMaintenance = false,
            Level = 1,
            Currency = Domain.Guest.Enums.EAcceptedCurrencies.Dollar,
            Name = "Room test",
            Price = 100
        };

        var command = new CreateRoomCommand()
        {
            RoomRequest = new RoomRequest
            {
                RoomDto = roomDto
            }
        };

        var repoMock = new Mock<IRoomRepository>();
        repoMock.Setup(x => x.Create(It.IsAny<RoomEntity>()))
            .Returns(Task.FromResult(1));

        // When
        var handler = new CreateRoomCommandHandler(repoMock.Object);
        var res = await handler.Handle(command, CancellationToken.None);

        // Then
        Assert.NotNull(res);
        Assert.True(res.Success);
        Assert.NotNull(res.Data);
        Assert.AreEqual(res.Data.Id, 1);
    }
}