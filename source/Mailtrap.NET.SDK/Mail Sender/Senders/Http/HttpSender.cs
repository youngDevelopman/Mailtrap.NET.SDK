using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.Exceptions;
using Mailtrap.NET.SDK.MailSender.Extensions;
using Mailtrap.NET.SDK.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Mailtrap.NET.SDK.MailSender.Senders.Http
{
    internal class HttpSender : IMailSender
    {
        static HttpClient httpClient;
        static HttpSender() 
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2)
            };
            httpClient = new HttpClient(socketsHandler);
        }

        public string Url { get; }
        private readonly HttpCredentials _credentials;
        public HttpSender(string url, HttpCredentials credentials)
        {
            Url = url ?? throw new ArgumentNullException($"{nameof(Url)} parameter cannot be null");
            _credentials = credentials ?? throw new ArgumentNullException($"Credentials parameter cannot be null");
            AddHeaders(httpClient);
        }

        public async Task SendAsync(SendEmailRequest sendEmailRequest, CancellationToken cancellationToken)
        {
            var mailtrapRequestModel = await sendEmailRequest.MapToHttpCompliantModelAsync();
            var result = await httpClient.PostAsJsonAsync(Url, mailtrapRequestModel, cancellationToken);
            
            if (!result.IsSuccessStatusCode)
            {
                var message = await result.Content.ReadAsStringAsync();
                throw new EmailSendingException($"Email sending over http has failed. Details: {message}");
            }
        }

        private void AddHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _credentials.Token);
        }
    }
}
