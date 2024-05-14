using Mailtrap.NET.SDK.MailSender.Extensions;
using Mailtrap.NET.SDK.Models;
using System.Net.Http.Json;

namespace Mailtrap.NET.SDK.MailSender.Senders.Http
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

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer 83f3eebabcdbb72920219e8262a8c740");
            var mailtrapRequestModel = await sendEmailRequest.MapToHttpCompliantModelAsync();
            var result = await httpClient.PostAsJsonAsync("api/send", mailtrapRequestModel, cancellationToken);
            var message = await result.Content.ReadAsStringAsync();
        }
    }
}
