namespace Mailtrap.NET.SDK.Configuration
{
    public class TransactionalStreamConfiguration
    {
        // Both credentials are optional, because the client code might utilize only one of the senders
        public HttpConfiguration HttpConfiguration { get; }
        public SmtpConfiguration SmtpConfiguration { get; }

        public TransactionalStreamConfiguration(
            HttpCredentials httpCredentials,
            SmtpCredentials smtpCredentials)
        {
            if(httpCredentials != null)
            {
                string url = $"https://{Constants.TRANSACTIONAL_HTTP_HOST}/send/api";
                HttpConfiguration = new HttpConfiguration(url, httpCredentials);
            }

            if(smtpCredentials != null)
            {
                SmtpConfiguration = new SmtpConfiguration(Constants.TRANSACTIONAL_SMTP_HOST, Constants.TRANSACTIONAL_SMTP_PORT, smtpCredentials);
            }
        }
    }
}
