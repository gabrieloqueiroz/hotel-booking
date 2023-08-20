using Application.Booking.DTOs;
using Application.Booking.Ports.In;
using Application.Booking.Request;
using Application.Booking.Response;
using Application.Payment.Response;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports.Out;
using Domain.Guest.Ports.Out;
using Domain.Room.Ports.Out;

namespace Application.Booking;

public class BookingManager : IBookingManager
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IPaymentProcessorFactory _paymentProcessorFactory;


    public BookingManager(
        IBookingRepository bookingRepository,
        IGuestRepository guestRepository,
        IRoomRepository roomRepository,
        IPaymentProcessorFactory paymentProcessorFactory)
    {
        _bookingRepository = bookingRepository;
        _guestRepository = guestRepository;
        _roomRepository = roomRepository;
        _paymentProcessorFactory = paymentProcessorFactory;
    }

    public async Task<BookingResponse> createBooking(BookingRequest bookingRequest)
    {
        /*
         * Esse método (createBooking) é o mesmo contigo na classe CreateBookingCommandHandler.
         * A intenção foi criar métodos de implementar o hexagonal utilizando o CQRS
         * E tambem com a não utilização do mesmo
         */

        try
        {

            var booking = BookingDto.MapToEntity(bookingRequest.data);
            booking.Guest = await _guestRepository.get(bookingRequest.data.GuestId);
            booking.Room = await _roomRepository.getAggregate(bookingRequest.data.RoomId);

            await booking.Save(_bookingRepository);

            bookingRequest.data.Id = booking.Id;

            return new BookingResponse
            {
                Success = true,
                data = bookingRequest.data
            };
        }
        catch (MissingBookingsInformationRequireds)
        {
            return new BookingResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.BOOKING_MISSING_REQUIRED_INFO,
                Message = "Missing required information passed to new booking"
            };
        }
        catch (CouldNotBookedException)
        {
            return new BookingResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.BOOKING_ROOM_COULD_NOT_BE_BOOKED,
                Message = "The selected room is not available"
            };
        };

    }

    public async Task<PaymentResponse> PaymentForABooking(PaymentRequestDto paymentRequestDto)
    {
        /*
        * Esse método (PaymentForABooking) é o mesmo contigo na classe PaymentForABookingCommandHandler.
        * A intenção foi criar métodos de implementar o hexagonal utilizando o CQRS
        * E tambem com a não utilização do mesmo
        */
        var paymentProcessor = _paymentProcessorFactory.GetPaymentProcessor(paymentRequestDto.SelectedPaymentProvider);

        var capturedPayment = await paymentProcessor.CapturePayment(paymentRequestDto.PaymentIntention);

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

    public async Task<BookingResponse> GetBooking(int id)
    {
        /*
         * Esse método (getBooking) é o mesmo contigo na classe GetBookingQueryHandler.
         * A intenção foi criar métodos de implementar o hexagonal utilizando o CQRS
         * E tambem com a não utilização do mesmo
         */
        try
        {
            var booking = await _bookingRepository.GetBooking(id);

            if (booking == null)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = EErrorCodes.ROOM_NOT_FOUND,
                    Message = "Room not found"
                };
            }

            return new BookingResponse
            {
                data = BookingDto.MapToDto(booking),
                Success = true
            };
        }
        catch(Exception)
        {
            return new BookingResponse
            {
                Success = false,
                ErrorCode = EErrorCodes.ROOM_NOT_FOUND,
                Message = "Room not found"
            };
        }
    }
}
