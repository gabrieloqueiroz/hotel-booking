namespace Application;

public enum EErrorCodes
{
    // Guest related code 1 to 99
    NOT_FOUND = 1,
    COULD_NOT_STORE_DATA = 2,
    INVALID_ID_PERSON = 3,
    MISSING_REQUIRED_INFO = 4,
    GUEST_NOT_FOUND = 5,

    // Room related code 100 to 199
    ROOM_FOUND = 101,
    ROOM_COULD_NOT_STORE_DATA = 102,
    ROOM_INVALID_ID_PERSON = 103,
    ROOM_MISSING_REQUIRED_INFO = 104,

    // Booking related code 200 to 299
    BOOKING_FOUND = 201,
    BOOKING_COULD_NOT_STORE_DATA = 202,
    BOOKING_INVALID_ID_PERSON = 203,
    BOOKING_MISSING_REQUIRED_INFO = 204,
    BOOKING_ROOM_COULD_NOT_BE_BOOKED = 205,

    // Booking related code 300 to 699
    INVALID_PAYMENT_INTENTION = 301,
}
