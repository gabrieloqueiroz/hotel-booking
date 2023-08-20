using Application.Guest.DTOs;
using Application.Room.DTOs;
using Domain.Booking.Entities;
using Domain.Guest.Enums;

namespace Application.Booking.DTOs;

public class BookingDto
{

    public BookingDto() 
    {
        this.PlacedAt = DateTime.UtcNow;
    }

    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int RoomId { get; set; }
    public int GuestId { get; set; }
    private EStatus Status { get; set; }

    public static BookingEntity MapToEntity(BookingDto bookingDto)
    {
        return new BookingEntity()
        {
            Id = bookingDto.Id,
            Start = bookingDto.Start,
            End = bookingDto.End,
            Guest = new Domain.Guest.Entities.GuestEntity { Id = bookingDto.GuestId },
            Room = new Domain.Room.Entities.RoomEntity { Id = bookingDto.RoomId },
            PlacedAt = bookingDto.PlacedAt,
        };
    }

    public static BookingDto MapToDto(BookingEntity booking)
    {
        return new BookingDto
        {
            Id = booking.Id,
            Start = booking.Start,
            End = booking.End,
            GuestId = booking.Guest.Id,
            RoomId = booking.Room.Id,
            PlacedAt = booking.PlacedAt,
            Status = booking.CurrentStatus
        };
    }
}