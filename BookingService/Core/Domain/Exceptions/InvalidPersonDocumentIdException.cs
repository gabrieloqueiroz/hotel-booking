namespace Domain.Exceptions;

public class InvalidPersonDocumentIdException : Exception
{
    public InvalidPersonDocumentIdException(string error) : base(error)
    {

    }

    public static void When(bool hasError, string message)
    {
        if (hasError) throw new InvalidPersonDocumentIdException(message);
    }
}
