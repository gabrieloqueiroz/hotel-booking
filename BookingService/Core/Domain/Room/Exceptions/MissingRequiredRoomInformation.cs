using Domain.Guest.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Room.Exceptions;

public class MissingRequiredRoomInformation : Exception
{
    public MissingRequiredRoomInformation(string message ) : base( message )
    {
    }

    public static void When(bool hasError, string message)
    {
        if (hasError) throw new MissingRequiredRoomInformation(message);
    }
}
