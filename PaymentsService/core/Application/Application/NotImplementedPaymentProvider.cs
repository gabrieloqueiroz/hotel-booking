using Application;
using Application.Payment.Ports.In;
using Application.Payment.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application;

public class NotImplementedPaymentProvider : IPaymentProcessor
{
    public Task<PaymentResponse> CapturePayment(string paymentIntention)
    {
        var paymentResponse = new PaymentResponse()
        {
            Success = false,
            ErrorCode = EErrorCodes.PAYMENT_PROVIDER_NOT_IMPLEMENTED,
            Message = "The selected payment provider is not available at the moment"
        };

        return Task.FromResult(paymentResponse);
    }
}
