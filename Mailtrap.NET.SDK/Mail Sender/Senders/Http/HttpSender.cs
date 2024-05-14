using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.MailSender.Extensions;
using Mailtrap.NET.SDK.Models;
using System.Net.Http.Json;

namespace Mailtrap.NET.SDK.MailSender.Senders.Http
{
    internal class HttpSender : IMailSender
    {
        public string Host { get; }
        private readonly HttpCredentials _credentials;
        public HttpSender(string host, HttpCredentials credentials)
        {
             Host = host;
            _credentials = credentials;
        }

        public async Task SendAsync(SendEmailRequest sendEmailRequest, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"https://{Host}"),
            };

            AddHeaders(httpClient);
            var mailtrapRequestModel = await sendEmailRequest.MapToHttpCompliantModelAsync();
            var result = await httpClient.PostAsJsonAsync("api/send", mailtrapRequestModel, cancellationToken);
            var message = await result.Content.ReadAsStringAsync();
        }

        private void AddHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_credentials.Token}");
        }
    }
}
