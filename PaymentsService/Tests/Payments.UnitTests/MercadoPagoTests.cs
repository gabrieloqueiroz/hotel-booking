using Application.MercadoPago;
using Application.Payment.Enum;
using Application;
using NUnit.Framework;
using Payment.Application;

namespace Payments.UnitTests;

public class MercadoPagoTests
{
    [Test]
    public void ShouldReturn_MercadoPagoAdapter_Provider()
    {
        // Given
        var factory = new PaymentProcessorFactory();

        // When
        var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

        // Then
        Assert.AreEqual(provider.GetType(), typeof(MercadoPagoAdapter));
    }

    [Test]
    public async Task Should_FailWhenPaymentIntentionStringIsInvalid()
    {
        // Given
        var factory = new PaymentProcessorFactory();

        // When
        var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago)
        var res = await provider.CapturePayment("");

        // Then
        Assert.False(res.Success);
        Assert.AreEqual(res.ErrorCode, EErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION);
        Assert.AreEqual(res.Message, "The selected payment intention is invalid");
    }

    [Test]
    public async Task Should_SuccessfullyProcessPayment()
    {
        // Given
        var factory = new PaymentProcessorFactory();

        // When
        var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);
        var res = await provider.CapturePayment("https://mercadopago.com.br/asdf");

        // Then
        Assert.IsTrue(res.Success);
        Assert.AreEqual(res.Message, "Payment successfully processed");
        Assert.NotNull(res.data);
        Assert.NotNull(res.data.CreatedDate);
        Assert.NotNull(res.data.PaymentId);
    }
}
