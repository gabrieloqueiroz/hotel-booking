namespace Domain.Guest.Exceptions;

public class MissingRequiredInformation : Exception
{
    public MissingRequiredInformation(string error) : base(error)
    {

    }

    public static void When(bool hasError, string message)
    {
        if (hasError) throw new MissingRequiredInformation(message);
    }
}
