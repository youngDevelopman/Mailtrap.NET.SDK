namespace Mailtrap.NET.SDK.Configuration
{
    public class HttpCredentials
    {
        public string Token { get; }

        public HttpCredentials(string token)
        {
            Token = token ?? throw new ArgumentNullException($"{nameof(Token)} parameter cannot be null");
        }
    }
}
