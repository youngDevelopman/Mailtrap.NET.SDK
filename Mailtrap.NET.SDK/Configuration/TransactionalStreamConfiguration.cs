namespace Mailtrap.NET.SDK.Configuration
{
    public class TransactionalStreamConfiguration
    {
        /* Parameters such as host and port have to be constant, 
        * otherwise client code might pass the custom host or port which can lead to unexpected errors or would make the library generic instead of being Mailtrap-oriented
        */
        public const string HTTP_HOST = "send.api.mailtrap.io";
        public const string SMTP_HOST = "live.smtp.mailtrap.io";
        public const int SMTP_PORT = 587;

        // Both credentials are optional, because the client code might utilize only one of the senders
        public HttpCredentials HttpCredentials { get; }
        public SmtpCredentials SmtpCredentials { get; }

        public TransactionalStreamConfiguration(
            HttpCredentials httpCredentials, 
            SmtpCredentials smtpCredentials)
        {
            HttpCredentials = httpCredentials;
            SmtpCredentials = smtpCredentials;
        }
    }
}
