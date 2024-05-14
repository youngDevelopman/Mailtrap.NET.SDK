using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.MailSender.Senders.Http;
using Mailtrap.NET.SDK.MailSender.Senders.Smtp;

namespace Mailtrap.NET.SDK.MailSender
{
    internal class MailSenderFactory : IMailSenderFactory
    {
        public TransactionalStreamConfiguration TransactionalStreamConfiguration { get; }
        public BulkStreamConfiguration BulkStreamConfiguration { get; }

        public MailSenderFactory(
            TransactionalStreamConfiguration transactionalStreamConfiguration, 
            BulkStreamConfiguration bulkStreamConfiguration) : this()
        {
            TransactionalStreamConfiguration = transactionalStreamConfiguration;
            BulkStreamConfiguration = bulkStreamConfiguration;
        }

        private readonly Dictionary<SenderOptions, Func<IMailSender>> _sendersMap;
        private MailSenderFactory()
        {
            _sendersMap = new()
            {
                {
                    SenderOptions.TransactionalSmtp,
                    () => TransactionalStreamConfiguration?.SmtpCredentials != null ?
                        new SmtpSender(TransactionalStreamConfiguration.SMTP_HOST, TransactionalStreamConfiguration.SMTP_PORT, TransactionalStreamConfiguration.SmtpCredentials) 
                        : throw new ArgumentNullException($"Smtp credentials for transactional stream are not specified") 
                },
                {
                    SenderOptions.TransactionalHttp,
                    () => TransactionalStreamConfiguration?.HttpCredentials != null ?
                        new HttpSender(TransactionalStreamConfiguration.HTTP_HOST, TransactionalStreamConfiguration.HttpCredentials)
                        : throw new ArgumentNullException($"Http credentials for transactional stream are not specified")
                },
                {
                    SenderOptions.BulkHttp,
                    () => BulkStreamConfiguration?.HttpCredentials != null ?
                        new HttpSender(BulkStreamConfiguration.HTTP_HOST, BulkStreamConfiguration.HttpCredentials)
                        : throw new ArgumentNullException($"Http credentials for bulk stream are not specified")
                },
                {
                    SenderOptions.BulkSmtp,
                    () => BulkStreamConfiguration?.SmtpCredentials != null ?
                        new SmtpSender(BulkStreamConfiguration.SMTP_HOST, BulkStreamConfiguration.SMTP_PORT, BulkStreamConfiguration.SmtpCredentials)
                        : throw new ArgumentNullException($"Smtp credentials for nulk stream are not specified")
                }
            };
        }

        public IMailSender GetMailSender(SenderOptions options)
        {
            return _sendersMap[options]();
        }
    }
}
