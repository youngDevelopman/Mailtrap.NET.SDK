# Mailtrap.NET.SDK

This repository is a limited implementation of [Mailtrap's sending API](https://api-docs.mailtrap.io/) for both live and test environments.

### Send email to live environment

```py
using var htmlReader = new StreamReader($"your_html_attachment.html");
using var textReader = new StreamReader($"your_txt_attachment.txt");


var client = new MailtrapClient(_transactionalStreamConfiguration, _bulkStreamConfiguration);
var email = new SendEmailRequest(
        new ParticipantInfo("sender's email", "sender's name"),
        new List<ParticipantInfo>() { new ParticipantInfo("receiver's email", "reciver's name") },
        "This is Email's subject",
        "This is emails body"
        )
{
    Attachments = DisposableStreamReaderList.FromList(new List<(StreamReader streamReader, string fileName)> { (htmlReader, "test.html"), (textReader, "text.txt") }),
    Html = "<b>Hello!<b>"
};
var cancellationToken = new CancellationTokenSource();
var options = SenderOptions.TransactionalHttp; // TransactionalHttp | TransactionalSmtp | BulkHttp | BulkSmtp
await client.SendEmailAsync(email, options, cancellationToken.Token);

```
