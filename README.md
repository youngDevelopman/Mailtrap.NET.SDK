# Mailtrap.NET.SDK

This repository is a limited implementation of [Mailtrap's sending API](https://api-docs.mailtrap.io/) for both live and test environments implemented by ```MailtrapClient```  and ```MailtrapTestClient``` respectively.

<b>To learn more about the implementation, please, read the following:</b> [Implementation details and architecture approaches](https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/master/docs/implementation-details.md)

### Send email to live environment
To send email to live environment use  ```MailtrapClient```. There are four options of what underlying communication type to use: ```TransactionalHttp```, ```TransactionalSmtp```, ```BulkHttp```, ```BulkSmtp```
```py
using var htmlReader = new StreamReader($"your_html_attachment.html");
using var textReader = new StreamReader($"your_txt_attachment.txt");


var client = new MailtrapClient(...transactionalStreamConfiguration, ...bulkStreamConfiguration);
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

### Send email to test environment
To send an email to test environment use  ```MailtrapTestClient```. There are two options of which underlying approach to use: ```Http```, ```Smtp``` (Bulk is not supported by test environment unlike a live one)
```py
using var htmlReader = new StreamReader($"your_html_attachment.html");
using var textReader = new StreamReader($"your_txt_attachment.txt");


var client = new MailtrapTestClient(...transactionalStreamConfiguration, ...bulkStreamConfiguration);
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
var options = TestSenderOptions.Http; // Http | Smtp
await client.SendEmailAsync(email, options, cancellationToken.Token);

```

## Dependency injection
If your project uses dependency injection there is a Mailtrap.NET.SDK.DependencyInjection.Extensions project that contains service collection extensions, so the dependency registration process would be much easier.

Use ```services.AddMailtrapClient()``` to register live client or ```services.AddMailtrapTestClient()``` to register test client
In order for extension to correctly read the configuration, please, add the following config schema with your credentials specified
```
{
  "Mailtrap": {
    "Client": {
      "Transactional": {
        "http": {
          "token": "your-transctional-http-token"
        },
        "smtp": {
          "port": 587,
          "user": "username",
          "password": "your-transctional-smtp-password"
        }
      },
      "Bulk": {
        "http": {
          "token": "your-bulk-http-password"
        },
        "smtp": {
          "port": 587,
          "user": "username",
          "password": "your-bulk-smtp-password"
        }
      }
    },
    "TestClient": {
      "http": {
        "inboxId": 123,
        "token": "your-test-client-password"
      },
      "smtp": {
        "port": 587,
        "user": "username",
        "password": "your-test-client-smtp-password"
      }
    }
  }
}
```
