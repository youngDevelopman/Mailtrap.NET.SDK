using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.Exceptions;
using Mailtrap.NET.SDK.MailSender;
using Mailtrap.NET.SDK.MailSender.Senders.Http;
using Mailtrap.NET.SDK.MailSender.Senders.Smtp;

namespace Mailtrap.NET.SDK.Unit.Tests
{
    public class MailSenderFactoryTests
    {
        [Theory]
        [InlineData(SenderOptions.TransactionalHttp, typeof(HttpSender))]
        [InlineData(SenderOptions.TransactionalSmtp, typeof(SmtpSender))]
        [InlineData(SenderOptions.BulkHttp, typeof(HttpSender))]
        [InlineData(SenderOptions.BulkSmtp, typeof(SmtpSender))]
        public void GetMailSender_PassAllAvailableSendersOptions_ResolveAllSendersAsExpected(SenderOptions options, Type type)
        {
            var httpCredentials = new HttpCredentials("token");
            var smtpCredentials = new SmtpCredentials("test_user", "qwerty123");
            var transactionConfig = new TransactionalStreamConfiguration(httpCredentials, smtpCredentials);
            var bulkConfig = new BulkStreamConfiguration(httpCredentials, smtpCredentials);

            var senderFactory = new MailSenderFactory(transactionConfig, bulkConfig);
            var sender = senderFactory.GetMailSender(options);
            Assert.Equal(sender.GetType(), type);
        }

        [Theory]
        [InlineData(SenderOptions.TransactionalHttp, typeof(HttpSender))]
        [InlineData(SenderOptions.TransactionalSmtp, typeof(SmtpSender))]
        [InlineData(SenderOptions.BulkHttp, typeof(HttpSender))]
        [InlineData(SenderOptions.BulkSmtp, typeof(SmtpSender))]
        public void GetMailSender_PassAllAvailableSendersOptionsWithoutSenderConfig_ThrowArgumentNullException(SenderOptions options, Type type)
        {
            var senderFactory = new MailSenderFactory(null, null);
            Assert.Throws<ArgumentNullException>(() => senderFactory.GetMailSender(options));
        }

        [Fact]
        public void GetMailSender_TryToResolveSenderThatDoesNotExists_ThrowSenderNotSupportedException()
        {
            var senderFactory = new MailSenderFactory(null, null);
            Assert.Throws<SenderNotSupportedException>(() => senderFactory.GetMailSender((SenderOptions)5));
        }
    }
}
