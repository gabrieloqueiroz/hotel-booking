using Application.Guest.Request;
using Application.Guest.Responses;

namespace Application.Guest.Ports;

public interface IGuestManager
{
    Task<GuestResponse> create(GuestRequest request);
}
