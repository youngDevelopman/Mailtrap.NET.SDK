namespace Mailtrap.NET.SDK.Configuration
{
    public class SmtpCredentials
    {
        public string User { get; }
        public string Password { get; }

        public SmtpCredentials(string user, string password)
        {
            User = user ?? throw new ArgumentNullException($"{nameof(User)} parameter cannot be null");
            Password = password ?? throw new ArgumentNullException($"{nameof(Password)} parameter cannot be null");
        }
    }
}
