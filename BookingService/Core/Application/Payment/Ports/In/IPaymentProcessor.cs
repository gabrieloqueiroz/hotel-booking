using Application.Payment.Response;

namespace Application.Payment.Ports.In;

public interface IPaymentProcessor
{
    Task<PaymentResponse> CapturePayment(string paymentIntention);
}
