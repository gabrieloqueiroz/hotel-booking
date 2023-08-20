using Application.Payment.Response;
using Domain.Booking.Ports.Out;
using MediatR;

namespace Application.Booking.CQRS.Commands;

public class PaymentForABookingHandler : IRequestHandler<PaymentForABookingCommand, PaymentResponse>
{
    private readonly IPaymentProcessorFactory _paymentProcessorFactory;


    public PaymentForABookingHandler(
        IPaymentProcessorFactory paymentProcessorFactory)
    {
        _paymentProcessorFactory = paymentProcessorFactory;
    }

    public async Task<PaymentResponse> Handle(PaymentForABookingCommand request, CancellationToken cancellationToken)
    {

        var payment = request.PaymentRequest;

        var paymentProcessor = _paymentProcessorFactory.GetPaymentProcessor(payment.SelectedPaymentProvider);

        var capturedPayment = await paymentProcessor.CapturePayment(payment.PaymentIntention);

        if (capturedPayment.Success)
        {
            return new PaymentResponse
            {
                Success = true,
                data = capturedPayment.data,
                Message = "Payment successfully processed"
            };
        }

        return capturedPayment;
    }
}