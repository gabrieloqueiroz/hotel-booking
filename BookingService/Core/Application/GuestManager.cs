using Application.Guest.DTOs;
using Application.Guest.Ports.In;
using Application.Guest.Request;
using Application.Guest.Responses;
using Domain.Exceptions;
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

            await guest.save(_guestRepository);

            request.Data.Id = guest.Id;

            return new GuestResponse
            {
                Data = request.Data,
                Success = true
            };
        }
        catch (InvalidPersonDocumentIdException)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.INVALID_ID_PERSON,
                Message = "The Id passed is not valid"
            };
        }
        catch (MissingRequiredInformation)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.MISSING_REQUIRED_INFO,
                Message = "Missing Required information passed"
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

    public async Task<GuestResponse> get(int guestId)
    {
       var guest = await _guestRepository.Get(guestId);

        if(guest == null)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.GUEST_NOT_FOUND,
                Message = $"The Id {guestId} not found"
            };
        }

        return new GuestResponse
        {
            Data = GuestDTO.mapToDto(guest),
            Success = true
        };
    }
}
