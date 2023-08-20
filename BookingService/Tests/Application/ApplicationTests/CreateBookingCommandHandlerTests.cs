using Application.Booking.CQRS.Commands;
using Application.Booking.DTOs;
using Application.Booking.Request;
using Domain.Booking.Entities;
using Domain.Booking.Ports.Out;
using Domain.Guest.Entities;
using Domain.Guest.Ports.Out;
using Domain.Room.Entities;
using Domain.Room.Ports.Out;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

public class CreateBookingCommandHandlerTests
{
    private CreateBookingCommandHandler GetCommandMock(
          Mock<IRoomRepository> roomRepository = null,
          Mock<IGuestRepository> guestRepository = null,
          Mock<IBookingRepository> bookingRepository = null
      )
    {
        var _bookingRepository = bookingRepository ?? new Mock<IBookingRepository>();
        var _guestRepository = guestRepository ?? new Mock<IGuestRepository>();
        var _roomRepository = roomRepository ?? new Mock<IRoomRepository>();

        return new CreateBookingCommandHandler(
            _bookingRepository.Object,
            _guestRepository.Object,
            _roomRepository.Object

        );
    }
    [Test]
    public async Task Should_Not_CreateBooking_If_Room_Is_Missing()
    {
        // Given
        var bookingDto = new BookingDto
        {
            // RoomId= 1,
            GuestId = 1,
            Start = DateTime.Now,
            End = DateTime.Now.AddDays(2),
        };

        var command = new CreateBookingCommand
        {
            BookingRequest = new BookingRequest
            {
                data = bookingDto
            }
        };

        var fakeGuest = new GuestEntity
        {
            Id = command.BookingRequest.data.GuestId,
            DocumentId = new Domain.Guest.ValueObjects.PersonId
            {
                DocumentType = Domain.Guest.Enums.EDocumentType.Passport,
                IdNumber = "abc1234"
            },
            Email = "a@a.com",
            Name = "Fake Guest",
            Surname = "Fake Guest Surname"
        };

        var guestRepository = new Mock<IGuestRepository>();
        guestRepository.Setup(x => x.get(command.BookingRequest.data.GuestId))
            .Returns(Task.FromResult(fakeGuest));

        var fakeRoom = new RoomEntity
        {
            Id = command.BookingRequest.data.RoomId,
            InMaintenance = false,
            Price = new Domain.Guest.ValueObjects.Price
            {
                Currency = Domain.Guest.Enums.EAcceptedCurrencies.Dollar,
                Value = 100
            },
            Name = "Fake Room 01",
            Level = 1,
        };

        var fakeBooking = new BookingEntity
        {
            Id = 1
        };

        var bookingRepository = new Mock<IBookingRepository>();
        bookingRepository.Setup(x => x.CreateBooking(It.IsAny<BookingEntity>()))
            .Returns(Task.FromResult(fakeBooking.Id));

        // When
        var handler = GetCommandMock(null, guestRepository, bookingRepository);
        var resp = await handler.Handle(command, CancellationToken.None);

        // Then
        Assert.NotNull(resp);
        Assert.False(resp.Success);
        Assert.AreEqual(resp.ErrorCode, Application.EErrorCodes.BOOKING_MISSING_REQUIRED_INFO);
        Assert.AreEqual(resp.Message, "Room is a required information");
    }

    [Test]
    public async Task Should_Not_CreateBooking_If_StartDateIsMissing()
    {
        // Given
        var bookingDto = new BookingDto
        {
            RoomId = 1,
            GuestId = 1,
            //Start = DateTime.Now,
            End = DateTime.Now.AddDays(2),
        };

        var command = new CreateBookingCommand
        {
            BookingRequest = new BookingRequest
            {
               data = bookingDto,
            }
        };

        var fakeGuest = new GuestEntity
        {
            Id = command.BookingRequest.data.GuestId,
            DocumentId = new Domain.Guest.ValueObjects.PersonId
            {
                DocumentType = Domain.Guest.Enums.EDocumentType.Passport,
                IdNumber = "abc1234"
            },
            Email = "a@a.com",
            Name = "Fake Guest",
            Surname = "Fake Guest Surname"
        };

        var guestRepository = new Mock<IGuestRepository>();
        guestRepository.Setup(x => x.get(command.BookingRequest.data.GuestId))
            .Returns(Task.FromResult(fakeGuest));

        var fakeRoom = new RoomEntity
        {
            Id = command.BookingRequest.data.RoomId,
            InMaintenance = false,
            Price = new Domain.Guest.ValueObjects.Price
            {
                Currency = Domain.Guest.Enums.EAcceptedCurrencies.Dollar,
                Value = 100
            },
            Name = "Fake Room 01",
            Level = 1,
        };

        var fakeBooking = new BookingEntity
        {
            Id = 1
        };

        var bookingRepository = new Mock<IBookingRepository>();
        bookingRepository.Setup(x => x.CreateBooking(It.IsAny<BookingEntity>()))
            .Returns(Task.FromResult(fakeBooking.Id));

        // When
        var handler = GetCommandMock(null, guestRepository, bookingRepository);
        var resp = await handler.Handle(command, CancellationToken.None);

        // Then
        Assert.NotNull(resp);
        Assert.False(resp.Success);
        Assert.AreEqual(resp.ErrorCode, Application.EErrorCodes.BOOKING_MISSING_REQUIRED_INFO);
        Assert.AreEqual(resp.Message, "Start is a required information");
    }

    [Test]
    public async Task Should_CreateBooking()
    {
        // Given
        var bookingDto = new BookingDto
        {
            RoomId = 1,
            GuestId = 1,
            Start = DateTime.Now,
            End = DateTime.Now.AddDays(2),
        };

        var command = new CreateBookingCommand
        {
            BookingRequest = new BookingRequest
            {
                data = bookingDto
            }
        };

        var fakeGuest = new GuestEntity
        {
            Id = command.BookingRequest.data.GuestId,
            DocumentId = new Domain.Guest.ValueObjects.PersonId
            {
                DocumentType = Domain.Guest.Enums.EDocumentType.Passport,
                IdNumber = "abc1234"
            },
            Email = "a@a.com",
            Name = "Fake Guest",
            Surname = "Fake Guest Surname"
        };

        var guestRepository = new Mock<IGuestRepository>();
        guestRepository.Setup(x => x.get(command.BookingRequest.data.GuestId))
            .Returns(Task.FromResult(fakeGuest));

        var fakeRoom = new RoomEntity
        {
            Id = command.BookingRequest.data.RoomId,
            InMaintenance = false,
            Price = new Domain.Guest.ValueObjects.Price
            {
                Currency = Domain.Guest.Enums.EAcceptedCurrencies.Dollar,
                Value = 100
            },
            Name = "Fake Room 01",
            Level = 1,
        };

        var roomRepository = new Mock<IRoomRepository>();
        roomRepository.Setup(x => x.getAggregate(command.BookingRequest.data.RoomId))
            .Returns(Task.FromResult(fakeRoom));

        var fakeBooking = new BookingEntity
        {
            Id = 1,
            Room = fakeRoom,
            Guest = fakeGuest,

        };

        var bookingRepoMock = new Mock<IBookingRepository>();
        bookingRepoMock.Setup(x => x.CreateBooking(It.IsAny<BookingEntity>()))
            .Returns(Task.FromResult(fakeBooking.Id));
        //bookingRepository.Setup(x => x.Save)

        // When
        var handler = GetCommandMock(roomRepository, guestRepository, bookingRepoMock);
        var resp = await handler.Handle(command, CancellationToken.None);

        // Then
        Assert.NotNull(resp);
        Assert.True(resp.Success);
        Assert.NotNull(resp.data);
        Assert.AreEqual(resp.data.Id, command.BookingRequest.data.Id);
    }
}