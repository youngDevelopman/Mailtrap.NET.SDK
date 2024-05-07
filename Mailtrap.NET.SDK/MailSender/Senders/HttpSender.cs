using Mailtrap.NET.SDK.Models;
using System.Net.Http.Json;

namespace Mailtrap.NET.SDK.MailSender.Senders
{
    internal class HttpSender : IMailSender
    {
        public HttpSender()
        {
             
        }

        public async Task SendAsync(SendEmailRequest sendEmailRequest, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://send.api.mailtrap.io"),
            };

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer test");
            var result = await httpClient.PostAsJsonAsync("api/send", sendEmailRequest, cancellationToken);
            var message = await result.Content.ReadAsStringAsync();
        }
    }
}
