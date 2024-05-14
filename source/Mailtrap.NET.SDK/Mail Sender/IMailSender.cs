using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK.MailSender
{
    internal interface IMailSender
    {
        Task SendAsync(SendEmailRequest sendEmailRequest, CancellationToken cancellationToken);
    }
}
