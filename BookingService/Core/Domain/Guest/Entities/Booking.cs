using Domain.Guest.Enums;
using Domain.Room.Entities;

namespace Domain.Guest.Entities;

public class Booking
{
    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RoomEntity Room { get; set; }
    public GuestEntity Guest { get; set; }
    private EStatus Status { get; set; }
    public EStatus CurrentStatus { get { return Status; } }

    public Booking()
    {
        Status = EStatus.Created;
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
}