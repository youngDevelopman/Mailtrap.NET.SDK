using Mailtrap.NET.SDK.MailSender;
using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK
{
    public class MailtrapClient
    {
        public async Task SendEmailAsync(SendEmailRequest email, CancellationToken cancellationToken)
        {
            var senderFactory = new MailSenderFactory();
            IMailSender sender = senderFactory.GetMailSender();

            await sender.SendAsync(email, cancellationToken);
        }
    }
}
