using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.Exceptions;
using Mailtrap.NET.SDK.MailSender.Senders.Http;
using Mailtrap.NET.SDK.MailSender.Senders.Smtp;

namespace Mailtrap.NET.SDK.MailSender
{
    internal class MailSenderFactory
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
                    () => TransactionalStreamConfiguration?.SmtpConfiguration != null ?
                        new SmtpSender(
                            TransactionalStreamConfiguration.SmtpConfiguration.Host, 
                            TransactionalStreamConfiguration.SmtpConfiguration.Port,
                            TransactionalStreamConfiguration.SmtpConfiguration.Credentials) 
                        : throw new ArgumentNullException($"Smtp credentials for transactional stream are not specified") 
                },
                {
                    SenderOptions.TransactionalHttp,
                    () => TransactionalStreamConfiguration?.HttpConfiguration != null ?
                        new HttpSender(
                            TransactionalStreamConfiguration.HttpConfiguration.Url, 
                            TransactionalStreamConfiguration.HttpConfiguration.Credentials)
                        : throw new ArgumentNullException($"Http credentials for transactional stream are not specified")
                },
                {
                    SenderOptions.BulkHttp,
                    () => BulkStreamConfiguration?.HttpConfiguration != null ?
                        new HttpSender(BulkStreamConfiguration.HttpConfiguration.Url, BulkStreamConfiguration.HttpConfiguration.Credentials)
                        : throw new ArgumentNullException($"Http credentials for bulk stream are not specified")
                },
                {
                    SenderOptions.BulkSmtp,
                    () => BulkStreamConfiguration?.SmtpConfiguration != null ?
                        new SmtpSender(BulkStreamConfiguration.SmtpConfiguration.Host,BulkStreamConfiguration.SmtpConfiguration.Port, BulkStreamConfiguration.SmtpConfiguration.Credentials)
                        : throw new ArgumentNullException($"Smtp credentials for bulk stream are not specified")
                }
            };
        }

        public IMailSender GetMailSender(SenderOptions options)
        {
            if(_sendersMap.TryGetValue(options, out var mailSender))
            {
                return mailSender();
            }
            throw new SenderNotSupportedException($"{Enum.GetName(options)} is not supported");
        }
    }
}
