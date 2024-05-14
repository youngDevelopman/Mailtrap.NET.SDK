// See https://aka.ms/new-console-template for more information
using Mailtrap.NET.SDK;
using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.Models;

Console.WriteLine("Hello, World!");

/*var httpCredentials = new HttpCredentials("83f3eebabcdbb72920219e8262a8c740");
var smtpCredentials = new SmtpCredentials("api", "83f3eebabcdbb72920219e8262a8c740");
var transactionalConfig = new TransactionalStreamConfiguration(httpCredentials, smtpCredentials);

var client = new MailtrapClient(transactionalConfig, null, SenderOptions.TransactionalSmtp);

using var htmlReader = new StreamReader($"test.html");
using var textReader = new StreamReader($"test.txt");

var email = new SendEmailRequest(
    new ParticipantInfo("mailtrap@demomailtrap.com", "MailTrap demo"),
    new List<ParticipantInfo>() { new ParticipantInfo("nazarmazurik99@gmail.com", "Nazar") },
    "test email",
    "sent from command line",
    null,
    new List<(StreamReader streamReader, string fileName)> { (htmlReader, "test.html"), (textReader, "text.txt") });

var ct = new CancellationToken();

await client.SendEmailAsync(email, ct);*/

async Task TestClient()
{
    using var htmlReader = new StreamReader($"test.html");
    using var textReader = new StreamReader($"test.txt");

    var smtpCredentials = new SmtpCredentials("bad5d32a3b8067", "57b15871236005");
    var httpCredentials = new HttpCredentials("1da4b1ba10f218011b2cd23b301e1cc4");
    var config = new TestClientConfiguration(2859709, httpCredentials, smtpCredentials);
    var client = new MailtrapTestClient(config, TestSenderOptions.Http);

    var email = new SendEmailRequest(
        new ParticipantInfo("mailtrap@demomailtrap.com", "MailTrap demo"),
        new List<ParticipantInfo>() { new ParticipantInfo("nazarmazurik99@gmail.com", "Nazar") },
        "test email",
        "sent from command line",
        null,
        new List<(StreamReader streamReader, string fileName)> { (htmlReader, "test.html"), (textReader, "text.txt") });
    var ct = new CancellationToken();
    await client.SendEmailAsync(email, ct);
}

await TestClient();
