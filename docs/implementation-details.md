# Implementation details and architecture approaches
## SMTP and HTTP senders
Regardless of which client is used - live or test, the email sending is done via HTTP or SMTP protocols, that is why the SDK has corresponding sender classes that would send email based on the configuration that is given.

- [HttpSender.cs](https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/master/source/Mailtrap.NET.SDK/Mail%20Sender/Senders/Http/HttpSender.cs)
- [SmtpSender.cs](https://github.com/youngDevelopman/Mailtrap.NET.SDK/tree/master/source/Mailtrap.NET.SDK/Mail%20Sender/Senders/Smtp/SmtpSender.cs)

Having these basic classes would give the flexibility to re-use them across different components such as email sending for live or test environments

### Http sender
HttpSender uses a static HTTP client with a pooled lifetime which is a [recommended way of using it by Microsoft](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use).
https://github.com/youngDevelopman/Mailtrap.NET.SDK/blob/e556b554224f9418b07071e9d6ff96b77b1c52ae/source/Mailtrap.NET.SDK/Mail%20Sender/Senders/Http/HttpSender.cs#L12-L20

### SMTP sender
SmtpSender uses [MailKit package](https://github.com/jstedfast/MailKit) to send emails. The library is also recommended by Microsoft since the standard SmtpClient is deprecated.
