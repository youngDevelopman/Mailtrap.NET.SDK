using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.MailSender;
using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK
{
    public class MailtrapClient : IMailtrapClient
    {
        private readonly MailSenderFactory _senderFactory;

        public MailtrapClient(
            TransactionalStreamConfiguration transactionalStreamConfiguration,
            BulkStreamConfiguration bulkStreamConfiguration)
        {
            // We promote tight coupling here, because we do not want to allow client's code to create its own sender factories
            _senderFactory = new MailSenderFactory(transactionalStreamConfiguration, bulkStreamConfiguration);
        }

        public async Task SendEmailAsync(SendEmailRequest email, SenderOptions options = SenderOptions.TransactionalHttp, CancellationToken cancellationToken = default)
        {
            IMailSender sender = _senderFactory.GetMailSender(options);

            await sender.SendAsync(email, cancellationToken);
        }
    }
}
