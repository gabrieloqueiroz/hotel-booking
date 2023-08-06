using Application.Guest.DTOs;
using Application.Guest.Request;
using Application.Guest.Responses;

namespace Application.Guest.Ports.In;

public interface IGuestManager
{
    Task<GuestResponse> create(GuestRequest request);
    Task<GuestResponse> get(int guestId);
}
