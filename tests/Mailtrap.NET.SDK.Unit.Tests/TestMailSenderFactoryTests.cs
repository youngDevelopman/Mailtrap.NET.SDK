using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.Exceptions;
using Mailtrap.NET.SDK.Mail_Sender;
using Mailtrap.NET.SDK.MailSender.Senders.Http;
using Mailtrap.NET.SDK.MailSender.Senders.Smtp;

namespace Mailtrap.NET.SDK.Unit.Tests
{
    public class SenderFactoriesTests
    {
        [Theory]
        [InlineData(TestSenderOptions.Http, typeof(HttpSender))]
        [InlineData(TestSenderOptions.Smtp, typeof(SmtpSender))]
        public void GetMailSender_PassAllAvailableSendersOptions_ResolveAllSendersAsExpected(TestSenderOptions options, Type type)
        {            
            var inboxId = 100;
            var httpCredentials = new HttpCredentials("token");
            var smtpCredentials = new SmtpCredentials("test_user", "qwerty123");
            var config = new TestClientConfiguration(inboxId, httpCredentials, smtpCredentials);
            var senderFactory = new TestMailSenderFactory(config);
            var sender = senderFactory.GetMailSender(options);
            Assert.Equal(sender.GetType(), type);
        }

        [Theory]
        [InlineData(TestSenderOptions.Http)]
        [InlineData(TestSenderOptions.Smtp)]
        public void GetMailSender_PassAllAvailableSendersOptionsWithoutSenderConfig_ThrowArgumentNullException(TestSenderOptions options)
        {
            var senderFactory = new TestMailSenderFactory(null);
            Assert.Throws<ArgumentNullException>(() => senderFactory.GetMailSender(options));
        }

        [Fact]
        public void GetMailSender_TryToResolveSenderThatDoesNotExists_ThrowSenderNotSupportedException()
        {
            var senderFactory = new TestMailSenderFactory(null);
            Assert.Throws<SenderNotSupportedException>(() => senderFactory.GetMailSender((TestSenderOptions)5));
        }


        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 100, null },
            new object[] { null, new HttpCredentials("token") }
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void TestClientConfiguration_DoNotSpecifyEitherInboxIdOrHttpCredentials_ThrowArgumentException(int? inboxId, HttpCredentials credentials)
        {
            Assert.Throws<ArgumentException>(() => new TestClientConfiguration(inboxId, credentials, null));
        }
    }
}
