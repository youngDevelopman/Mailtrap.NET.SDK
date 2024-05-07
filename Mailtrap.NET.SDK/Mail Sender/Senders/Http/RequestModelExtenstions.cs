using Mailtrap.NET.SDK.Models;
using System.Text;
using static Mailtrap.NET.SDK.Mail_Sender.Senders.Http.SendEmailMailtrapRequestModel;

namespace Mailtrap.NET.SDK.Mail_Sender.Senders.Http
{
    internal static class RequestModelExtenstions
    {
        public static async Task<SendEmailMailtrapRequestModel> MapToMailtrapModelAsync(this SendEmailRequest request)
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
    }
}
