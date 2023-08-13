using Domain.Guest.Exceptions;
using Domain.Guest.Ports.Out;
using Domain.Guest.ValueObjects;

namespace Domain.Guest.Entities;

public class GuestEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public PersonId DocumentId { get; set; }


    private void validateState()
    {
        InvalidPersonDocumentIdException.When(
            DocumentId == null ||
            string.IsNullOrEmpty(DocumentId.IdNumber) ||
            DocumentId.IdNumber.Length <= 3 ||
            DocumentId.DocumentType == 0,
            "The Document Id is invalid"
            );

        MissingRequiredInformation.When(
            string.IsNullOrEmpty(Name) ||
            string.IsNullOrEmpty(Surname) ||
            string.IsNullOrEmpty(Email),
            "Requireds fields not be empty"
            );
    }

    public void isValid()
    {
        validateState();
    }

    public async Task save(IGuestRepository guestRepository)
    {
        validateState();

        if (Id == 0)
        {
            Id = await guestRepository.Create(this);
        }
        else
        {
            //await guestRepository.Update(this);
        }
    }
}