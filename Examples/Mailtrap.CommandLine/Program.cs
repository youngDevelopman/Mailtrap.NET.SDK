// See https://aka.ms/new-console-template for more information
using Mailtrap.NET.SDK;
using Mailtrap.NET.SDK.Configuration;
using Mailtrap.NET.SDK.Models;

Console.WriteLine("Hello, World!");

var httpCredentials = new HttpCredentials("83f3eebabcdbb72920219e8262a8c740");
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

await client.SendEmailAsync(email, ct);
