using Domain.Exceptions;
using Domain.Ports.Out;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Guest
{
    public int Id { get; set; }
    public string Name{ get; set; }
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

    public async Task save(IGuestRepository guestRepository)
    {
        this.validateState();

        if(this.Id == 0)
        {
            this.Id = await guestRepository.Create(this);
        }
        else
        {
            //await guestRepository.Update(this);
        }
    }
}