namespace Mailtrap.NET.SDK.MailSender
{
    internal interface IMailSenderFactory
    {
        IMailSender GetMailSender(SenderOptions options);
    }
}
