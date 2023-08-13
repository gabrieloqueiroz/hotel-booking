using Domain.Guest.Entities;

namespace Domain.Guest.Ports.Out;

public interface IGuestRepository
{
    Task<GuestEntity> get(int id);
    Task<int> Create(GuestEntity guest);
}
