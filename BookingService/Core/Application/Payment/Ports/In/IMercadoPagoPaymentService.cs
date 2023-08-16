using Application.Payment.Dtos;
using Application.Payment.Response;

namespace Application.Payment.Ports.In;

public interface IMercadoPagoPaymentService
{
    Task<PaymentResponse> PayWithCreditCard(string paymentIntention);
    Task<PaymentResponse> PayWithDebitCard(string paymentIntention);
    Task<PaymentResponse> PayTransferAccount(string transferIntention);
}
