using Domain.Guest.ValueObjects;
using Domain.Room.Exceptions;
using Domain.Room.Ports.Out;

namespace Domain.Room.Entities;

public class RoomEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public Price Price { get; set; }
    public bool IsAvailable
    {
        get
        {
            if (InMaintenance || HasGuest) return false;
            return true;
        }
    }
    public bool HasGuest
    {
        //TODO: Verificar se existem Bookins abertos para esta Room
        get { return true; }
    }


    private void validateState()
    {

        MissingRequiredRoomInformation.When(
            string.IsNullOrEmpty(Name),
            "Requireds fields not be empty"
            );
    }

    public async Task save(IRoomRepository roomRepository)
    {
        validateState();

        if (Id == 0)
        {
            Id = await roomRepository.Create(this);
        }
        else
        {
            //await guestRepository.Update(this);
        }
    }
}
