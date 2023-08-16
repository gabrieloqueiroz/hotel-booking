using Application.Payment.Enum;

namespace Application.Payment.Dtos;

public class PaymentStateDto
{
    public Status Status { get; set; }
    public string PaymentId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Message { get; set; }
}
