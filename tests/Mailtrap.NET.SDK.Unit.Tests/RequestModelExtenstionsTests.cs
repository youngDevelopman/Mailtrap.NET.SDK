using Mailtrap.NET.SDK.MailSender.Extensions;
using Mailtrap.NET.SDK.Models;
using MimeKit;

namespace Mailtrap.NET.SDK.Unit.Tests
{
    public class RequestModelExtenstionsTests
    {
        [Fact]
        public async Task MapToHttpCompliantModelAsync_PassEmailWithHtmlAndAttachments_ShouldMapSuccesfully()
        {
            // Arrange
            using var htmlReader = new StreamReader($"Test files/HTML_Test.html");
            using var textReader = new StreamReader($"Test files/Text_Test.txt");

            var to = new List<ParticipantInfo>(new List<ParticipantInfo>() { new ParticipantInfo("nazar@gmail.com", "Nazar") });
            var email = new SendEmailRequest(
                new ParticipantInfo("mailtrap@demomailtrap.com", "MailTrap demo"),
                to,
                "test email",
                "sent from command line",
                "<b>Hello!<b>",
                new List<(StreamReader streamReader, string fileName)> { (htmlReader, "test.html"), (textReader, "text.txt") });

            // Act
            var result = await email.MapToHttpCompliantModelAsync();

            // Assert
            Assert.Equal(result.From.Name, email.From.Name);
            Assert.Equal(result.From.Email, email.From.Email);
            Assert.Collection(
                result.To,
                item => { Assert.Equal(item.Name, to[0].Name); Assert.Equal(item.Email, to[0].Email); });
            Assert.Equal(result.Subject, email.Subject);
            Assert.Equal(result.Text, email.Text);
            Assert.Equal(result.Html, email.Html);

            var resultAttachments = result.Attachments.ToList();
            using var htmlResultReader = new StreamReader($"Test files/HTML_Test.html");
            using var textResultReader = new StreamReader($"Test files/Text_Test.txt");

            var incomingHtmlAttachment = await htmlResultReader.ReadToEndAsync();
            var incomingTextAttachment = await textResultReader.ReadToEndAsync();

            var htmlResultAttachementContent = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(resultAttachments[0].Content));
            var textResultAttachementContent = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(resultAttachments[1].Content));

            Assert.Equal(htmlResultAttachementContent, incomingHtmlAttachment);
            Assert.Equal(textResultAttachementContent, incomingTextAttachment);
        }

        [Fact]
        public async Task MapToSmtpCompliantModelAsync_PassEmailWithHtmlAndAttachments_ShouldMapSuccesfully()
        {
            // Arrange
            using var htmlReader = new StreamReader($"Test files/HTML_Test.html");
            using var textReader = new StreamReader($"Test files/Text_Test.txt");

            var to = new List<ParticipantInfo>(new List<ParticipantInfo>() { new ParticipantInfo("nazar@gmail.com", "Nazar") });
            var email = new SendEmailRequest(
                new ParticipantInfo("mailtrap@demomailtrap.com", "MailTrap demo"),
                to,
                "test email",
                "sent from command line",
                "<b>Hello!<b>",
                new List<(StreamReader streamReader, string fileName)> { (htmlReader, "test.html"), (textReader, "text.txt") });

            // Act
            var result = await email.MapToSmtpCompliantModelAsync();

            // Assert
            Assert.Equal(result.From[0].Name, email.From.Name);
            Assert.Equal(result.From.Mailboxes.First().Address, email.From.Email);

            var recepientsList = email.To.ToList();
            Assert.Equal(result.To[0].Name, recepientsList[0].Name);
            Assert.Equal(result.To.Mailboxes.First().Address, recepientsList[0].Email);

            Assert.Equal(result.Subject, email.Subject);
            Assert.Equal(result.TextBody, email.Text);
            Assert.Equal(result.HtmlBody, email.Html);

            var resultAttachments = result.Attachments.Select(x => (MimePart)x).ToList();
            Assert.Equal(resultAttachments[0].FileName, email.Attachments[0].fileName);
            Assert.Equal(resultAttachments[1].FileName, email.Attachments[1].fileName);
        }
    }
}