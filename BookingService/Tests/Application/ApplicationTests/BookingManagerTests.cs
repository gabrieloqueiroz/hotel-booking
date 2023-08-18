using Application.Booking;
using Application.Booking.DTOs;
using Application.Payment.Dtos;
using Application.Payment.Enum;
using Application.Payment.Ports.In;
using Application.Payment.Response;
using Domain.Booking.Ports.Out;
using Domain.Guest.Ports.Out;
using Domain.Room.Ports.Out;
using Moq;
using NUnit.Framework;

namespace ApplicationTests;

public class BookingManagerTests
{
    [Test]
    public async Task Should_PayForABooking()
    {
        // Given
        var dto = new PaymentRequestDto
        {
            SelectedPaymentProvider = SupportedPaymentProviders.MercadoPago,
            PaymentIntention = "https://www.mercadopago.com.br/asdf",
            SelectedPaymentMethod = SupportedPaymentMethods.CreditCard
        };

        var bookingRepository = new Mock<IBookingRepository>();
        var roomRepository = new Mock<IRoomRepository>();
        var guestRepository = new Mock<IGuestRepository>();
        var paymentProcessorFactory = new Mock<IPaymentProcessorFactory>();
        var paymentProcessor = new Mock<IPaymentProcessor>();

        var responseDto = new PaymentStateDto
        {
            CreatedDate = DateTime.Now,
            Message = $"Successfully paid {dto.PaymentIntention}",
            PaymentId = "123",
            Status = Status.Success
        };

        var response = new PaymentResponse
        {
            data = responseDto,
            Success = true,
            Message = "Payment successfully processed"
        };

        paymentProcessor.
            Setup(x => x.CapturePayment(dto.PaymentIntention))
            .Returns(Task.FromResult(response));

        paymentProcessorFactory
            .Setup(x => x.GetPaymentProcessor(dto.SelectedPaymentProvider))
            .Returns(paymentProcessor.Object);

        var bookingManager = new BookingManager(
            bookingRepository.Object,
            guestRepository.Object,
            roomRepository.Object,
            paymentProcessorFactory.Object);

        // When
        var res = await bookingManager.PaymentForABooking(dto);

        // Then
        Assert.NotNull(res);
        Assert.True(res.Success);
        Assert.AreEqual(res.Message, "Payment successfully processed");
    }
}
