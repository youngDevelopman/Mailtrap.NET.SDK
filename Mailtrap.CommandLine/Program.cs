// See https://aka.ms/new-console-template for more information
using Mailtrap.NET.SDK;
using Mailtrap.NET.SDK.Models;

Console.WriteLine("Hello, World!");

var client = new MailtrapClient();

var email = new SendEmailRequest(
    new ParticipantInfo("mailtrap@demomailtrap.com", "MailTrap demo"),
    new List<ParticipantInfo>() { new ParticipantInfo("nazarmazurik99@gmail.com", "Nazar") },
    "test email",
    "sent from command line", 
    null,
    null);

var ct = new CancellationToken();

await client.SendEmailAsync(email, ct);
