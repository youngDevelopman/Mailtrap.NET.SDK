using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.MailSender.Extensions;
using Mailtrap.NET.SDK.Models;
using System.Net.Http.Json;

namespace Mailtrap.NET.SDK.MailSender.Senders.Http
{
    internal class HttpSender : IMailSender
    {
        public string Url { get; }
        private readonly HttpCredentials _credentials;
        public HttpSender(string url, HttpCredentials credentials)
        {
            Url = url ?? throw new ArgumentNullException($"{nameof(Url)} parameter cannot be null");
            _credentials = credentials ?? throw new ArgumentNullException($"Credentials parameter cannot be null");
        }

        public async Task SendAsync(SendEmailRequest sendEmailRequest, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(Url),
            };

            AddHeaders(httpClient);
            var mailtrapRequestModel = await sendEmailRequest.MapToHttpCompliantModelAsync();
            var result = await httpClient.PostAsJsonAsync(string.Empty, mailtrapRequestModel, cancellationToken);
            var message = await result.Content.ReadAsStringAsync();
        }

        private void AddHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_credentials.Token}");
        }
    }
}
