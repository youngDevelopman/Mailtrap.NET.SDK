using Mailtrap.NET.SDK.Models;

namespace Mailtrap.NET.SDK
{
    public interface IMailtrapTestClient
    {
        Task SendEmailAsync(SendEmailRequest email, TestSenderOptions testSenderOptions = TestSenderOptions.Http, CancellationToken cancellationToken = default);
    }
}
