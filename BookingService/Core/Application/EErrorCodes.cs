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
}
