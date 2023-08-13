namespace Domain.Booking.Exceptions;

public class MissingBookingsInformationRequireds : Exception
{
    public MissingBookingsInformationRequireds(string message) : base(message) { } 

    public static void When(bool hasError, string message)
    {
        if (hasError) throw new MissingBookingsInformationRequireds(message);
    }
}
