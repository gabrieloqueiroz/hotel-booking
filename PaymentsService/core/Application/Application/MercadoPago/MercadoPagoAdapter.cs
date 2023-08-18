using Application.MercadoPago.Exceptions;
using Application.Payment.Dtos;
using Application.Payment.Enum;
using Application.Payment.Ports.In;
using Application.Payment.Response;

namespace Application.MercadoPago;

public class MercadoPagoAdapter : IPaymentProcessor
{
    public Task<PaymentResponse> CapturePayment(string paymentIntention)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(paymentIntention))
            {
                throw new InvalidPaymentIntentionException();
            }

            paymentIntention += "/success";

            var dto = new PaymentStateDto
            {
                CreatedDate = DateTime.Now,
                Message = $"Successfully paid {paymentIntention}",
                PaymentId = "123",
                Status = Status.Success
            };

            var response = new PaymentResponse
            {
                data = dto,
                Success = true,
                Message = "Payment successfully processed"
            };

            return Task.FromResult(response);
        }
        catch (InvalidPaymentIntentionException)
        {
            var resp = new PaymentResponse()
            {
                Success = false,
                ErrorCode = EErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION,
                Message = "The selected payment intention is invalid"
            };
            return Task.FromResult(resp);
        }
    }
}
