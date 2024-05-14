namespace Mailtrap.NET.SDK.Configuration
{
    public class TestClientConfiguration
    {
        // Http related config
        public const string SMTP_HOST = "sandbox.smtp.mailtrap.io";
        public const int SMTP_PORT = 587;
        public SmtpCredentials SmtpCredentials { get; }

        // Http related config
        private const string HTTP_HOST = "sandbox.api.mailtrap.io";
        public HttpCredentials HttpCredentials { get; }
        public int? InboxId { get; }
        public string Url 
        {
            get 
            {
                return $"https://{HTTP_HOST}/api/send/{InboxId}";
            }  
        }

        public TestClientConfiguration(int? inboxId, HttpCredentials httpCredentials, SmtpCredentials smtpCredentials)
        {
            if((inboxId == null) != (httpCredentials == null)) 
            {
                throw new ArgumentException($"Both inbox id and http credentials have to be specified.");
            }

            InboxId = inboxId;
            HttpCredentials = httpCredentials;
            SmtpCredentials = smtpCredentials;
        }
    }
}
