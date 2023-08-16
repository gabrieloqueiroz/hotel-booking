using Application.Payment.Dtos;

namespace Application.Payment.Response;

public class PaymentResponse : Application.Response
{
    public PaymentStateDto data { get; set; }
}
