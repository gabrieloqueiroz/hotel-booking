using Domain.Guest.Entities;
using Domain.Guest.Enums;

namespace Application.Guest.DTOs;

public class GuestDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string IdNumber { get; set; }
    public int IdTypeCode { get; set; }

    public static GuestEntity mapToEntity(GuestDTO guestDto)
    {
        return new GuestEntity
        {
            Id = guestDto.Id,
            Name = guestDto.Name,
            Surname = guestDto.Surname,
            Email = guestDto.Email,
            DocumentId = new Domain.Guest.ValueObjects.PersonId
            {
                DocumentType = EDocumentType.Driverlicence,
                IdNumber = guestDto.IdNumber,
            }
        };
    }

    public static GuestDTO mapToDto(GuestEntity guest)
    {
        return new GuestDTO
        {
            Id = guest.Id,
            Name = guest.Name,
            Surname = guest.Surname,
            Email = guest.Email,
        };
    }
}
