using Application.MercadoPago.Exceptions;
using Application.Payment.Dtos;
using Application.Payment.Ports.In;
using Application.Payment.Response;

namespace Application.MercadoPago;

public class MercadoPagoAdapter : IMercadoPagoPaymentService
{
    public Task<PaymentResponse> PayTransferAccount(string transferIntention)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentResponse> PayWithCreditCard(string paymentIntention)
    {
        try
        {
            if (string.IsNullOrEmpty(paymentIntention)) throw new InvalidPaymentException("Invalid payment intention");

            paymentIntention += "/success";

            var paymentResult = new PaymentStateDto
            {
                CreatedDate = DateTime.Now,
                Message = $"Successfully paid - {paymentIntention}",
                PaymentId = "123",
                Status = Payment.Enum.Status.Success
            };

            var response = new PaymentResponse
            {
                Success = true,
                data = paymentResult
            };

            return Task.FromResult(response);
        }
        catch (Exception)
        {
            var result = new PaymentResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.INVALID_PAYMENT_INTENTION
            };

            return Task.FromResult(result);
        }
    }

    public Task<PaymentResponse> PayWithDebitCard(string paymentIntention)
    {
        throw new NotImplementedException();
    }
}
