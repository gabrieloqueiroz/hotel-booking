using Application.Booking.DTOs;
using Application.Payment.Response;
using MediatR;

namespace Application.Booking.CQRS.Commands;

public class PaymentForABookingCommand : IRequest<PaymentResponse>
{
    public PaymentRequestDto PaymentRequest { get; set; }
}