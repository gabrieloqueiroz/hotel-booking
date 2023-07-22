using Application.Guest.DTOs;

namespace Application.Guest.Responses;

public class GuestResponse : Response
{
    public GuestDTO Data;

    public bool isMappedException()
    {
        return Enum.GetValues(typeof(EErrorCodes)).Cast<EErrorCodes>().ToHashSet().Contains(this.ErrorCode);
    }
}
