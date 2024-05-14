using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.MailSender;
using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK
{
    public class MailtrapClient
    {
        private readonly SenderOptions _options;
        private readonly IMailSenderFactory _senderFactory;

        public MailtrapClient(
            TransactionalStreamConfiguration transactionalStreamConfiguration, 
            BulkStreamConfiguration bulkStreamConfiguration) : this(transactionalStreamConfiguration, bulkStreamConfiguration, SenderOptions.TransactionalHttp) 
        { 
        }

        public MailtrapClient(
            TransactionalStreamConfiguration transactionalStreamConfiguration,
            BulkStreamConfiguration bulkStreamConfiguration,
            SenderOptions senderOptions)
        {
            // We promote tight coupling here, because we do not want to allow user create its own sender factories
            _senderFactory = new MailSenderFactory(transactionalStreamConfiguration, bulkStreamConfiguration);
            _options = senderOptions;
        }

        public async Task SendEmailAsync(SendEmailRequest email, CancellationToken cancellationToken)
        {
            IMailSender sender = _senderFactory.GetMailSender(_options);

            await sender.SendAsync(email, cancellationToken);
        }
    }
}
