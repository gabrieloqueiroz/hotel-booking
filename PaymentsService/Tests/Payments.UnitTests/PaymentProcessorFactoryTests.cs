using Application;
using Application.MercadoPago;
using Application.Payment.Enum;
using NUnit.Framework;
using Payment.Application;

namespace Payments.UnitTests;

public class PaymentProcessorFactoryTests
{
    [Test]
    public void ShouldReturn_NotImplementedPaymentProvider_WhenAskingForStripeProvider()
    {
        // Given
        var factory = new PaymentProcessorFactory();
        
        // When
        var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.Stripe);

        // Then
        Assert.AreEqual(provider.GetType(), typeof(NotImplementedPaymentProvider));
    }

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
    public async Task ShouldReturnFalse_WhenCapturingPaymentFor_NotImplementedPaymentProvider()
    {
        // Given
        var factory = new PaymentProcessorFactory();

        //When
        var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.Stripe);

        var res = await provider.CapturePayment("https://myprovider.com/asdf");

        // Then
        Assert.False(res.Success);
        Assert.AreEqual(res.ErrorCode, EErrorCodes.PAYMENT_PROVIDER_NOT_IMPLEMENTED);
        Assert.AreEqual(res.Message, "The selected payment provider is not available at the moment");
    }
}