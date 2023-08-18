using Application.Payment.Enum;

namespace Application.Booking.DTOs;

public class PaymentRequestDto
{
    public int BookingId { get; set; }
    public string PaymentIntention { get; set; }
    public SupportedPaymentProviders SelectedPaymentProvider { get; set; }
    public SupportedPaymentMethods SelectedPaymentMethod { get; set; }
}
