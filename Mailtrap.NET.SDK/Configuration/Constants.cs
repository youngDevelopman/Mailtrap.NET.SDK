namespace Mailtrap.NET.SDK.Configuration
{
    internal static class Constants
    {
        /* Parameters such as host and port have to be constant, 
            * otherwise client code might pass the custom host or port which can lead to unexpected errors or would make the library generic instead of being Mailtrap-oriented
        */

        public const string TRANSACTIONAL_HTTP_HOST = "send.api.mailtrap.io";
        public const string TRANSACTIONAL_SMTP_HOST = "live.smtp.mailtrap.io";
        public const int TRANSACTIONAL_SMTP_PORT = 587;

        public const string BULK_HTTP_HOST = "bulk.api.mailtrap.io";
        public const string BULK_SMTP_HOST = "bulk.smtp.mailtrap.io";
        public const int BULK_SMTP_PORT = 587;

        public const string TEST_HTTP_HOST = "sandbox.api.mailtrap.io";
        public const string TEST_SMTP_HOST = "sandbox.smtp.mailtrap.io";
        public const int TEST_SMTP_PORT = 587;
    }
}
