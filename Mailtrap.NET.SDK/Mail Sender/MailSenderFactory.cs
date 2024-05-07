using Mailtrap.NET.SDK.MailSender.Senders;
using Mailtrap.NET.SDK.MailSender.Senders.Http;

namespace Mailtrap.NET.SDK.MailSender
{
    internal class MailSenderFactory : IMailSenderFactory
    {
        public IMailSender GetMailSender()
        {
            return new HttpSender();
        }
    }
}
