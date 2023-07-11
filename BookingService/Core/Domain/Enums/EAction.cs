using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums;

public enum EAction
{
    Pay = 0,
    Finish = 1, // after paid and used
    Cancel = 2, // can nerver be paid
    Refound = 3, // Paid then refound
    Reopen = 4 // canceled
}
