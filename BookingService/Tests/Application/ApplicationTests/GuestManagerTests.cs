using Application;
using Application.Guest.DTOs;
using Application.Guest.Request;
using Application.Guest.Responses;
using Domain.Entities;
using Domain.Ports.Out;
using Moq;
using NUnit.Framework;
using System;

namespace ApplicationTests
{
    /**
     * Nesse modelo de teste criamos uma teste herdando da inteface de repositorio
     * e forçamos o retorno dela com o esperado
     */
    //public class FakeRepo : IGuestRepository
    //{
    //    public Task<int> Create(Guest guest)
    //    {
    //        return Task.FromResult(1111);
    //    }

    //    public Task<Guest> Get(int id)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class GuestManagerTests
    {

        GuestManager _guestManager;
        Mock<IGuestRepository> guestRepository = new Mock<IGuestRepository>();

        [SetUp]
        public void Setup()
        {
            //this._guestManager = new GuestManager(new FakeRepo());  -- Aqui fazemos o uso da classe Fake
            _guestManager = new GuestManager(guestRepository.Object);

        }

        [Test]
        public async Task should_save_guest()
        {
            // Given
            var expectedId = 111;
            var guestDTO = new GuestDTO
            {
                Name = "Test",
                Surname = "Test",
                Email = "Test@test.com",
                IdNumber = "1234",
                IdTypeCode = 1
            };

            var request = new GuestRequest
            {
                Data = guestDTO
            };
                        

            guestRepository.Setup(x => x.Create(It.IsAny<Guest>()))
                .Returns(Task.FromResult(expectedId)); // Nesse modo usamos a lib Moq para criar o Mock desse classe

            
            // When
            var response = await _guestManager.create(request);

            // Then
            Assert.NotNull(response);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(expectedId, response.Data.Id);
            Assert.AreEqual(response.Data.Name, guestDTO.Name);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        public async Task should_invalidate_request_to_documentId(string doc)
        {
            // Given
            var guestDTO = new GuestDTO
            {
                Name = "Test",
                Surname = "Test",
                Email = "Test@test.com",
                IdNumber = doc,
                IdTypeCode = 1
            };
            var request = new GuestRequest
            {
                Data = guestDTO
            };


            guestRepository.Setup(x => x.Create(It.IsAny<Guest>()))
                .Returns(Task.FromResult(111));


            // When
            var response = await _guestManager.create(request);

            // Then
            Assert.IsNotNull(response);
            Assert.False(response.Success);
            Assert.AreEqual(EErrorCodes.INVALID_ID_PERSON, response.ErrorCode);
            Assert.AreEqual("The Id passed is not valid", response.Message);
        }

        [TestCase("", "surname", "email")]
        [TestCase("name", "", "email")]
        [TestCase("name", "surname", "")]
        [TestCase(null, "surname", "email")]
        [TestCase("name", "surname", null)]
        [TestCase("name", null, "email")]
        [TestCase("name", null, "")]
        public async Task should_invalidate_request_to_documentId(string name, string surname, string email)
        {
            // Given
            var guestDTO = new GuestDTO
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };
            var request = new GuestRequest
            {
                Data = guestDTO
            };


            guestRepository.Setup(x => x.Create(It.IsAny<Guest>()))
                .Returns(Task.FromResult(111));


            // When
            var response = await _guestManager.create(request);

            // Then
            Assert.IsNotNull(response);
            Assert.False(response.Success);
            Assert.AreEqual(EErrorCodes.MISSING_REQUIRED_INFO, response.ErrorCode);
            Assert.AreEqual("Missing Required information passed", response.Message);
        }

        [Test]
        public async Task should_return_guestNoFound_when_gues_does_not_exist()
        {
            //Given
            var guestRequest = new Guest
            {
                Id = 333,
                Name = "Test",
            };

            guestRepository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(Task.FromResult<Guest>(null));

            //When
            var response = await _guestManager.get(guestRequest.Id);

            //Then
            Assert.IsNotNull(response);
            Assert.False(response.Success);
            Assert.AreEqual(EErrorCodes.GUEST_NOT_FOUND, response.ErrorCode);
            Assert.AreEqual($"The Id {guestRequest.Id} not found", response.Message);
        }

        [Test]
        public async Task should_return_guest_with_success()
        {
            //Given
            var guest = new Guest
            {
                Id = 333,
                Name = "Test",
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    DocumentType = Domain.Enums.EDocumentType.Driverlicence,
                    IdNumber = "123"
                }
            };

            guestRepository.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(Task.FromResult((Guest?)guest));

            //When
            var response = await _guestManager.get(guest.Id);

            //Then
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(guest.Id, response.Data.Id);
            Assert.AreEqual(guest.Name, response.Data.Name);
        }
    }
}