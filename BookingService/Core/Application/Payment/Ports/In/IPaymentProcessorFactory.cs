using Application.Payment.Enum;
using Application.Payment.Ports.In;

namespace Domain.Booking.Ports.Out;

public interface IPaymentProcessorFactory
{
    IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders selectedPaymentProviders);
}
