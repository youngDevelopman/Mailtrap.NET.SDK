using Mailtrap.NET.SDK.Mail_Sender.Senders.Http;
using Mailtrap.NET.SDK.Models;
using MimeKit;
using System.Text;
using static Mailtrap.NET.SDK.Mail_Sender.Senders.Http.SendEmailMailtrapRequestModel;

namespace Mailtrap.NET.SDK.MailSender.Extensions;

internal static class RequestModelExtenstions
{
    public static async Task<SendEmailMailtrapRequestModel> MapToHttpCompliantModelAsync(this SendEmailRequest request)
    {
        using var attachmentStreamReades = request.Attachments;
        
        var attachments = new List<Attachment>();
        foreach (var attachment in attachmentStreamReades)
        {
            var content = await attachment.reader.ReadToEndAsync();
            var type = Path.GetExtension(attachment.fileName);

            var mappedAttachment = new Attachment(
                Convert.ToBase64String(Encoding.UTF8.GetBytes(content)),
                attachment.fileName,
                type);
            attachments.Add(mappedAttachment);
        }

        var from = new SendEmailMailtrapRequestModel.ParticipantInfo(request.From.Email, request.From.Name);
        var to = request.To.Select(x => new SendEmailMailtrapRequestModel.ParticipantInfo(x.Email, x.Name)).ToList();

        var mailtrapModel = new SendEmailMailtrapRequestModel(from, to, request.Subject, request.Text, request.Html, attachments);

        return mailtrapModel;
    }

    public static async Task<MimeMessage> MapToSmtpCompliantModelAsync(this SendEmailRequest request)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(request.From.Name, request.From.Email));
        message.Subject = request.Subject;
        foreach (var reciever in request.To)
        {
            message.To.Add(new MailboxAddress(reciever.Name, reciever.Email));
        }

        var body = new TextPart("plain")
        {
            Text = request.Text,
        };

        var multipart = new Multipart("mixed");
        multipart.Add(body);

        using var attachmentStreamReades = request.Attachments;
        foreach (var attachment in attachmentStreamReades)
        {
            var extenstion = Path.GetExtension(attachment.fileName);

            /*  We do not need to use 'using' here because the stream that is used by MimeContent object
                would be disposed by MimeContent object itself (it implements IDisposable).
                Otherwise, the exception would be riased saying that the file was closed.
            */
            var memoryStream = new MemoryStream();
            await attachment.reader.BaseStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            var attachmentToAdd = new MimePart("text", extenstion)
            {
                Content = new MimeContent(memoryStream, ContentEncoding.Base64),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(attachment.fileName)
            };
            multipart.Add(attachmentToAdd);
        }

        message.Body = multipart;
        
        return message;
    }
}
