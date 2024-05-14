namespace Mailtrap.NET.SDK.Configuration
{
    public class BulkStreamConfiguration
    {
        // Both credentials are optional, because the client code might utilize only one of the senders
        public HttpConfiguration HttpConfiguration { get; }
        public SmtpConfiguration SmtpConfiguration { get; }

        public BulkStreamConfiguration(
            HttpCredentials httpCredentials,
            SmtpCredentials smtpCredentials)
        {
            if (httpCredentials != null)
            {
                string url = $"https://{Constants.BULK_HTTP_HOST}/send/api";
                HttpConfiguration = new HttpConfiguration(url, httpCredentials);
            }

            if(smtpCredentials != null) 
            {
                SmtpConfiguration = new SmtpConfiguration(Constants.BULK_SMTP_HOST, Constants.BULK_SMTP_PORT, smtpCredentials);
            }
        }
    }
}
