using Domain.Booking.Entities;
using Domain.Guest.Enums;
using Domain.Guest.ValueObjects;
using Domain.Room.Exceptions;
using Domain.Room.Ports.Out;

namespace Domain.Room.Entities;

public class RoomEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public Price Price { get; set; }
    public ICollection<BookingEntity> Bookings { get; set; }
    public bool IsAvailable
    {
        get
        {
            if (InMaintenance || HasGuest) return false;
            return true;
        }
    }
    public bool HasGuest
    {
        get
        {
            var notAvailableStatuses = new List<EStatus>
            {
                EStatus.Created,
                EStatus.Paid
            };

            return this.Bookings.Where(
                b => b.Room.Id == this.Id &&
                notAvailableStatuses.Contains(b.CurrentStatus)).Count() > 0;
        }
    }


    private void validateState()
    {

        MissingRequiredRoomInformation.When(
            string.IsNullOrEmpty(Name),
            "Requireds fields not be empty"
            );
    }

    public bool canBeBooked()
    {
        try
        {
            this.validateState();
        }
        catch (Exception)
        {
            return false;
        }

        if (!this.IsAvailable)
        {
            return false;
        }

        return true;
    }


    public async Task save(IRoomRepository roomRepository)
    {
        validateState();

        if (Id == 0)
        {
            Id = await roomRepository.Create(this);
        }
        else
        {
            //await guestRepository.Update(this);
        }
    }
}
