using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.DataStructures;
using Mailtrap.NET.SDK.Models;
using Microsoft.Extensions.Configuration;

namespace Mailtrap.NET.SDK.Functional.Tests
{
    public class MailtrapClientTests
    {
        private TransactionalStreamConfiguration _transactionalStreamConfiguration;
        private BulkStreamConfiguration _bulkStreamConfiguration;
        private string _from;
        private string _to;
        public MailtrapClientTests()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<MailtrapClientTests>();
            var configuration = builder.Build();

            // Transactional
            var transactional = configuration.GetSection("Mailtrap:Client:Transactional");
            var transactionalHttp = transactional.GetSection("http");
            var transactionalSmtp = transactional.GetSection("smtp");

            var transactionalHttpToken = transactionalHttp.GetSection("token").Value;

            var transactionalSmtpUser = transactionalSmtp.GetSection("user").Value;
            var transactionalSmtpPassword = transactionalSmtp.GetSection("password").Value;

            var transactionalHttpCredentials = new HttpCredentials(transactionalHttpToken);
            var transactionalsmtpCredentials = new SmtpCredentials(transactionalSmtpUser, transactionalSmtpPassword);
            _transactionalStreamConfiguration = new TransactionalStreamConfiguration(transactionalHttpCredentials, transactionalsmtpCredentials);


            // Bulk
            var bulk = configuration.GetSection("Mailtrap:Client:Bulk");
            var bulkHttp = bulk.GetSection("http");
            var bulkSmtp = bulk.GetSection("smtp");

            var bulkHttpToken = bulkHttp.GetSection("token").Value;

            var bulkSmtpUser = bulkSmtp.GetSection("user").Value;
            var bulkSmtpPassword = bulkSmtp.GetSection("password").Value;

            var bulkHttpCredentials = new HttpCredentials(bulkHttpToken);
            var bulksmtpCredentials = new SmtpCredentials(bulkSmtpUser, bulkSmtpPassword);
            _bulkStreamConfiguration = new BulkStreamConfiguration(bulkHttpCredentials, bulksmtpCredentials);

            _from = configuration.GetRequiredSection("from").Value;
            _to = configuration.GetRequiredSection("to").Value;
        }

        [Theory]
        [InlineData(SenderOptions.TransactionalHttp)]
        [InlineData(SenderOptions.TransactionalSmtp)]
        [InlineData(SenderOptions.BulkHttp)]
        [InlineData(SenderOptions.BulkSmtp)]
        public async Task SendEmailAsync_PassEmailWithAttachments_ShouldBeOk(SenderOptions options)
        {
            using var htmlReader = new StreamReader($"Test files/HTML_Test.html");
            using var textReader = new StreamReader($"Test files/Text_Test.txt");


            var client = new MailtrapClient(_transactionalStreamConfiguration, _bulkStreamConfiguration);
            var email = new SendEmailRequest(
                    new ParticipantInfo(_from, "MailTrap demo"),
                    new List<ParticipantInfo>() { new ParticipantInfo(_to, "Nazar") },
                    "test email",
                    "sent from command line"
                    )
            {
                Attachments = DisposableStreamReaderList.FromList(new List<(StreamReader streamReader, string fileName)> { (htmlReader, "test.html"), (textReader, "text.txt") }),
                Html = "<b>Hello!<b>"
            };
            var cancellationToken = new CancellationTokenSource();
            await client.SendEmailAsync(email, options, cancellationToken.Token);
        }
    }
}