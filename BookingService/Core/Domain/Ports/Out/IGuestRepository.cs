using Domain.Entities;

namespace Domain.Ports.Out;

public interface IGuestRepository
{
    Task<Guest> Get(int id);
    Task<int> Create(Guest guest);
}
