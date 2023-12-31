﻿using Application.Booking.DTOs;
using Application.Booking.Response;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports.Out;
using Domain.Guest.Ports.Out;
using Domain.Room.Ports.Out;
using MediatR;

namespace Application.Booking.CQRS.Commands;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IRoomRepository _roomRepository;


    public CreateBookingCommandHandler(
        IBookingRepository bookingRepository,
        IGuestRepository guestRepository,
        IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _guestRepository = guestRepository;
        _roomRepository = roomRepository;
    }

    public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bookingRequest = request.BookingRequest;

            var booking = BookingDto.MapToEntity(bookingRequest.data);
            booking.Guest = await _guestRepository.get(bookingRequest.data.GuestId);
            booking.Room = await _roomRepository.getAggregate(bookingRequest.data.RoomId);

            await booking.Save(_bookingRepository);

            bookingRequest.data.Id = booking.Id;

            return new BookingResponse
            {
                Success = true,
                data = bookingRequest.data
            };
        }
        catch (MissingBookingsInformationRequireds)
        {
            return new BookingResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.BOOKING_MISSING_REQUIRED_INFO,
                Message = "Missing required information passed to new booking"
            };
        }
        catch (CouldNotBookedException)
        {
            return new BookingResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.BOOKING_ROOM_COULD_NOT_BE_BOOKED,
                Message = "The selected room is not available"
            };
        };
    }
}