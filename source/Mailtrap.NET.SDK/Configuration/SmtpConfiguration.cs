namespace Mailtrap.NET.SDK.Configuration
{
    public class SmtpConfiguration
    {
        public string Host { get; }
        public int Port { get; }
        public SmtpCredentials Credentials { get; }

        public SmtpConfiguration(string host, int port, SmtpCredentials credentials)
        {
            Port = port;
            Host = host ?? throw new ArgumentNullException($"{nameof(Host)} parameter cannot be null");
            Credentials = credentials ?? throw new ArgumentNullException($"{nameof(Credentials)} parameter cannot be null");
        }
    }
}
