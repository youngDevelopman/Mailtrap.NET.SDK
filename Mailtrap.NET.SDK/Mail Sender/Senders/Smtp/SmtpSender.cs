using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK.MailSender.Senders.Smtp
{
    internal class SmtpSender : IMailSender
    {
        public Task SendAsync(SendEmailRequest sendEmailRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
