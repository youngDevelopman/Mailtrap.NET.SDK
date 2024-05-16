# Implementation details and architecture approaches
### SMTP and HTTP senders
Regardless of which client is used - live or test, the email sending is done via HTTP or SMTP protocols, that is why the SDK has corresponding sender classes that would send email based on the configuration that is given.

- [HttpSender.cs](https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/master/source/Mailtrap.NET.SDK/Mail%20Sender/Senders/Http/HttpSender.cs)
- [SmtpSender.cs](https://github.com/youngDevelopman/Mailtrap.NET.SDK/tree/master/source/Mailtrap.NET.SDK/Mail%20Sender/Senders/Smtp/SmtpSender.cs)

Having these basic classes would give the flexibility to re-use them across different components such as email sending for live or test environments

### Http sender
HttpSender uses a static HTTP client with a pooled lifetime which is a [recommended way of using it by Microsoft](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use).
https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/e556b554224f9418b07071e9d6ff96b77b1c52ae/source/Mailtrap.NET.SDK/Mail%20Sender/Senders/Http/HttpSender.cs#L12-L20

### SMTP sender
SmtpSender uses [MailKit package](https://github.com/jstedfast/MailKit) to send emails. The library is also recommended by Microsoft since the standard SmtpClient is deprecated.

### Sender factories
Since both Smtp and Http senders are heavily used with a variety of different configurations it was decided to add factory methods to construct mail senders:
- [MailSenderFactory](https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/master/source/Mailtrap.NET.SDK/Mail%20Sender/MailSenderFactory.cs)
- [TestMailSenderFactory](https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/master/source/Mailtrap.NET.SDK/Mail%20Sender/TestMailSenderFactory.cs)

### Client classes
In order to send emails to Mailtrap client-side code can use [MailtrapClient](https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/master/source/Mailtrap.NET.SDK/MailtrapClient.cs) (to send live emails) or [TestMailtrapClient](https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/master/source/Mailtrap.NET.SDK/MailtrapTestClient.cs) (to send test emails).
Both allow the client to define which transport method to use.

### DisposableStreamReaderList
Custom [DisposableStreamReaderList](https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/master/source/Mailtrap.NET.SDK/Data%20Structures/DisposableStreamReaderList.cs) class is used in [email request object](https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/master/source/Mailtrap.NET.SDK/Models/SendEmailRequest.cs) to store .NET Streams of attachment files.
The main benefit of using this collection is that it implements Disposable pattern to dispose of all Streams that are contained within itself.

###  Models' creation
All models either public (i.e. SendEmailRequest) or internal (i.e. SendEmailMailtrapRequestModel) have to follow the rules:
- Be immutable
- Has to accept required parameters via constructor
- Has to accept optional parameter via property (better use init keyword to achieve singleton-like behavior for properties)
- Model has to validate itself

Example: [SendEmailRequest.cs](https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/master/source/Mailtrap.NET.SDK/Models/SendEmailRequest.cs)
Having such rules applied to all models would give us a better consistency in our code base, make easier for client code to use the SDK and for developers to extend the code base.
