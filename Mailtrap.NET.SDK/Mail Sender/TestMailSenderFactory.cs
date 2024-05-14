using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.Exceptions;
using Mailtrap.NET.SDK.MailSender;
using Mailtrap.NET.SDK.MailSender.Senders.Http;
using Mailtrap.NET.SDK.MailSender.Senders.Smtp;

namespace Mailtrap.NET.SDK.Mail_Sender
{
    internal class TestMailSenderFactory
    {
        public TestClientConfiguration TestClientConfiguration { get; set; }
        public TestMailSenderFactory(TestClientConfiguration configuration)
        {
            TestClientConfiguration = configuration;
        }

        private readonly Dictionary<TestSenderOptions, Func<IMailSender>> _sendersMap;

        private TestMailSenderFactory()
        {
            _sendersMap = new()
            {
                {
                    TestSenderOptions.Smtp,
                    () => TestClientConfiguration?.SmtpConfiguration != null ?
                        new SmtpSender(
                            TestClientConfiguration.SmtpConfiguration.Host,
                            TestClientConfiguration.SmtpConfiguration.Port,
                            TestClientConfiguration.SmtpConfiguration.Credentials)
                        : throw new ArgumentNullException($"Smtp credentials for the test http flow are not specified")
                },
                {
                    TestSenderOptions.Http,
                    () => TestClientConfiguration?.HttpConfiguration != null ?
                        new HttpSender(
                            TestClientConfiguration.HttpConfiguration.Url,
                            TestClientConfiguration.HttpConfiguration.Credentials)
                        : throw new ArgumentNullException($"Http credentials for the test smtp flow are not specified")
                }
            };
        }

        public IMailSender GetMailSender(TestSenderOptions options)
        {
            if (_sendersMap.TryGetValue(options, out var mailSender))
            {
                return mailSender();
            }
            throw new SenderNotSupportedException($"{Enum.GetName(options)} is not supported");
        }
    }
}
