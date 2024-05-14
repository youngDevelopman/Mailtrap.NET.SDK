using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.Mail_Sender;
using Mailtrap.NET.SDK.MailSender;
using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK
{
    public class MailtrapTestClient : IMailtrapTestClient
    {
        private readonly TestSenderOptions _senderOptions;
        private readonly TestMailSenderFactory _senderFactory;
        public MailtrapTestClient(TestClientConfiguration configuration, TestSenderOptions senderOptions)
        {
            _senderOptions = senderOptions;

            // We promote tight coupling here, because we do not want to allow client's code to create its own sender factories
            _senderFactory = new TestMailSenderFactory(configuration);
        }

        public async Task SendEmailAsync(SendEmailRequest email, TestSenderOptions options = TestSenderOptions.Http, CancellationToken cancellationToken = default)
        {
            IMailSender sender = _senderFactory.GetMailSender(_senderOptions);

            await sender.SendAsync(email, cancellationToken);
        }
    }
}
