using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    private EStatus Status { get; set; }
    public EStatus CurrentStatus { get { return this.Status; } }

    public void ChangeState(EAction action)
    {
        this.Status = (this.Status, action) switch
        {
            (EStatus.Created,  EAction.Pay)      => EStatus.Paid,
            (EStatus.Created,  EAction.Cancel)   => EStatus.Canceled,
            (EStatus.Paid,     EAction.Finish)   => EStatus.Finished,
            (EStatus.Paid,     EAction.Refound)  => EStatus.Refounded,
            (EStatus.Canceled, EAction.Reopen)   => EStatus.Created,
              _ => this.Status
        };
    }
}