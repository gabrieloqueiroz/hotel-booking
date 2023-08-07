using Domain.Guest.Enums;
using Domain.Room.Entities;

namespace Application.Room.DTOs;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public decimal Price { get; set; }
    public EAcceptedCurrencies Currency { get; set; }

    public static RoomEntity MapToEntity(RoomDto dto)
    {
        return new RoomEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            Level = dto.Level,
            InMaintenance = dto.InMaintenance,
            Price = new Domain.Guest.ValueObjects.Price
            {
                Currency = dto.Currency,
                Value = dto.Price
            }
        };
    }
}
