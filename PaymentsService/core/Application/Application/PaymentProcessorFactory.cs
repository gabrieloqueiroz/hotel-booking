using Application.MercadoPago;
using Application.Payment.Enum;
using Application.Payment.Ports.In;
using Domain.Booking.Ports.Out;

namespace Payment.Application;

public class PaymentProcessorFactory : IPaymentProcessorFactory
{
    public IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders selectedPaymentProviders)
    {
        switch (selectedPaymentProviders)
        {
            case SupportedPaymentProviders.MercadoPago:
                return new MercadoPagoAdapter();

            default: return new NotImplementedPaymentProvider();
        }
    }
}
