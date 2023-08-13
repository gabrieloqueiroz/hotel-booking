using Domain.Booking.Exceptions;
using Domain.Booking.Ports.Out;
using Domain.Guest.Entities;
using Domain.Guest.Enums;
using Domain.Room.Entities;

namespace Domain.Booking.Entities;

public class BookingEntity
{
    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RoomEntity Room { get; set; }
    public GuestEntity Guest { get; set; }
    private EStatus Status { get; set; }
    public EStatus CurrentStatus { get { return Status; } }

    public BookingEntity()
    {
        Status = EStatus.Created;
        PlacedAt = DateTime.UtcNow;
    }

    public void ChangeState(EAction action)
    {
        Status = (Status, action) switch
        {
            (EStatus.Created, EAction.Pay) => EStatus.Paid,
            (EStatus.Created, EAction.Cancel) => EStatus.Canceled,
            (EStatus.Paid, EAction.Finish) => EStatus.Finished,
            (EStatus.Paid, EAction.Refound) => EStatus.Refounded,
            (EStatus.Canceled, EAction.Reopen) => EStatus.Created,
            _ => Status
        };
    }

    public async Task Save(IBookingRepository bookingRepository)
    {
        validateState();

        if (Id == 0)
        {
            Id = await bookingRepository.CreateBooking(this);
        }
        else
        {
            //await guestRepository.Update(this);
        }
    }

    private void validateState()
    {
        MissingBookingsInformationRequireds.When(
            this.PlacedAt == default ||
            this.Start == default ||
            this.End == default ||
            this.Room == null ||
            this.Guest == null,
            "Requireds fields not be empty"
            );

        this.Guest.isValid();

        if (!this.Room.canBeBooked())
        {
            throw new CouldNotBookedException("The room selected is not available");
        }
    }
}