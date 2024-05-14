using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.Exceptions;
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
            switch (_senderOptions)
            {
                case TestSenderOptions.Http:
                    sender = new HttpSender(_configuration.HttpConfiguration.Url, _configuration.HttpConfiguration.Credentials);
                    break;
                case TestSenderOptions.Smtp:
                    sender = new SmtpSender(_configuration.SmtpConfiguration.Host, _configuration.SmtpConfiguration.Port, _configuration.SmtpConfiguration.Credentials);
                    break;
                default:
                    throw new SenderNotSupportedException($"{Enum.GetName(_senderOptions)} is not supported");
            }
            
            await sender.SendAsync(email, cancellationToken);
        }
    }
}
