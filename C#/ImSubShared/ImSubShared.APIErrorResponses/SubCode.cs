namespace ImSubShared.APIErrorResponses
{
    public enum SubCode
    {
        // Generic codes (from -1 to 99)
        INTERNAL_ERROR = -1,
        OK = 0,
        INVALID_MODEL = 1,
        FIELD_REQUIRED = 2,

        // Telegram error codes (from 100 to 199)
        TELEGRAM_INVALID_DATA = 100,
        TELEGRAM_ID_NOT_FOUND = 101,

        // Authentication error codes (from 200 to 299)
        REFRESH_TOKEN_REQUIRED = 200,
        INVALID_REFRESH_TOKEN = 201,
        MAIL_ALREADY_PRESENT = 202,

        // User codes (from 300 to 399)
        USER_ALREADY_REGISTERED = 300
        
    }
}
