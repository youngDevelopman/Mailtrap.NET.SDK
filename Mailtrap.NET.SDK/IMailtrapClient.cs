using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK
{
    public interface IMailtrapClient
    {
        Task SendEmailAsync(SendEmailRequest email, SenderOptions options = SenderOptions.TransactionalHttp, CancellationToken cancellationToken = default);
    }
}
