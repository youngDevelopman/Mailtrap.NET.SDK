using MailKit.Net.Smtp;
using Mailtrap.NET.SDK.MailSender.Extensions;
using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK.MailSender.Senders.Smtp
{
    internal class SmtpSender : IMailSender
    {
        public SmtpSender()
        {
            
        }

        public async Task SendAsync(SendEmailRequest sendEmailRequest, CancellationToken cancellationToken)
        {
            var message = sendEmailRequest.MapToSmtpCompliantModel();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("live.smtp.mailtrap.io", 587, MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);

                // Note: only needed if the SMTP server requires authentication
                await client.AuthenticateAsync("api", "83f3eebabcdbb72920219e8262a8c740", cancellationToken);

                await client.SendAsync(message, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
            }
        }
    }
}
