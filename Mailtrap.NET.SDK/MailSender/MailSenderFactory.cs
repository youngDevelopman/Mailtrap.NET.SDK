using Mailtrap.NET.SDK.MailSender.Senders;

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
