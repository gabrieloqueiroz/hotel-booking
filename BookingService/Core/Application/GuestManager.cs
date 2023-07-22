using Application.Guest.DTOs;
using Application.Guest.Ports.In;
using Application.Guest.Request;
using Application.Guest.Responses;
using Domain.Ports.Out;

namespace Application;

public class GuestManager : IGuestManager
{
    private IGuestRepository _guestRepository;

    public GuestManager(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<GuestResponse> create(GuestRequest request)
    {
        try
        {
            var guest = GuestDTO.mapToEntity(request.Data);

            request.Data.Id = await _guestRepository.Create(guest);

            return new GuestResponse
            {
                Data = request.Data,
                Success = true
            };
        }
        catch (Exception)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.COULD_NOT_STORE_DATA,
                Message = "There was an error when saving to DB"
            };
        }
    }
}
