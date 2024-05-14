using MailKit.Net.Smtp;
using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.MailSender.Extensions;
using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK.MailSender.Senders.Smtp
{
    internal class SmtpSender : IMailSender
    {
        public string Host { get; }
        public int Port { get; }
        private readonly SmtpCredentials _credentials;
        public SmtpSender(string host, int port, SmtpCredentials credentials)
        {
            Host = host;
            Port = port;
            _credentials = credentials;
        }

        public async Task SendAsync(SendEmailRequest sendEmailRequest, CancellationToken cancellationToken)
        {
            var message = await sendEmailRequest.MapToSmtpCompliantModelAsync();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(Host, Port, MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);

                await client.AuthenticateAsync(_credentials.User, _credentials.Password, cancellationToken);

                await client.SendAsync(message, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
            }
        }
    }
}
