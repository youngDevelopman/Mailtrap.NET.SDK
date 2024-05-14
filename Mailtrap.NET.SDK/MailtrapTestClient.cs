using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.MailSender;
using Mailtrap.NET.SDK.MailSender.Senders.Http;
using Mailtrap.NET.SDK.MailSender.Senders.Smtp;
using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK
{
    public class MailtrapTestClient
    {
        private readonly TestClientConfiguration _configuration;
        private readonly TestSenderOptions _senderOptions;
        public MailtrapTestClient(TestClientConfiguration configuration, TestSenderOptions senderOptions)
        {
            _configuration = configuration;
            _senderOptions = senderOptions;
        }

        public async Task SendEmailAsync(SendEmailRequest email, CancellationToken cancellationToken)
        {
            IMailSender sender;
            if(_senderOptions == TestSenderOptions.Http)
            {
                sender = new HttpSender(_configuration.Url, _configuration.HttpCredentials);
            }
            else
            {
                sender = new SmtpSender(TestClientConfiguration.SMTP_HOST, TestClientConfiguration.SMTP_PORT, _configuration.SmtpCredentials);
            }

            await sender.SendAsync(email, cancellationToken);
        }
    }
}
