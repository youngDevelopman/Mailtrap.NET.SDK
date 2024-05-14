namespace Mailtrap.NET.SDK.Configuration
{
    public class TestClientConfiguration
    {
        // Both credentials are optional, because the client code might utilize only one of the senders
        public HttpConfiguration HttpConfiguration { get; }
        public SmtpConfiguration SmtpConfiguration { get; }
        public int? InboxId { get; }

        public TestClientConfiguration(int? inboxId, HttpCredentials httpCredentials, SmtpCredentials smtpCredentials)
        {
            // If either inboxId or httpCredentials is set and the other is not, then raise an exception, because both are required to construct the right url
            if((inboxId == null) != (httpCredentials == null)) 
            {
                throw new ArgumentException($"Both inbox id and http credentials have to be specified.");
            }

            if(inboxId != null && httpCredentials != null) 
            {
                string url = $"https://{Constants.TEST_HTTP_HOST}/api/send/{inboxId}";
                InboxId = inboxId;
                HttpConfiguration = new HttpConfiguration(url, httpCredentials);
            }

            if(smtpCredentials != null)
            {
                SmtpConfiguration = new SmtpConfiguration(Constants.TEST_SMTP_HOST, Constants.TEST_SMTP_PORT, smtpCredentials);
            }
        }
    }
}
