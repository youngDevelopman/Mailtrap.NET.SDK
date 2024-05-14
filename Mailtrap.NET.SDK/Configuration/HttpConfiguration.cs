namespace Mailtrap.NET.SDK.Configuration
{
    public class HttpConfiguration
    {
        public string Url { get; }
        public HttpCredentials Credentials { get; }

        public HttpConfiguration(string url, HttpCredentials credentials)
        {
            Url = url ?? throw new ArgumentNullException($"{nameof(Url)} parameter cannot be null");
            Credentials = credentials ?? throw new ArgumentNullException($"{nameof(Credentials)} parameter cannot be null");
        }
    }
}
