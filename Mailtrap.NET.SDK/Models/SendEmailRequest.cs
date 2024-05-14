using Mailtrap.NET.SDK.DataStructures;

namespace Mailtrap.NET.SDK.Models
{
    public class SendEmailRequest
    {
        public ParticipantInfo From { get; }

        public IReadOnlyCollection<ParticipantInfo> To { get; }

        public string Subject { get; }

        public string Text { get; }

        public string Html { get; }

        public DisposableStreamReaderList Attachments { get; } = new DisposableStreamReaderList();

        public SendEmailRequest(
            ParticipantInfo from, 
            IReadOnlyCollection<ParticipantInfo> to, 
            string subject, 
            string text, 
            string html,
            List<(StreamReader reader, string fileName)> attachments)
        {
            // Required parameters
            From = from ?? throw new ArgumentNullException($"{nameof(From)} parameter cannot be null");

            To = to ?? throw new ArgumentNullException($"{nameof(To)} parameter cannot be null");
            if(To.Count <= 0) 
            {
                throw new ArgumentException($"The amount of recepients has to be more or equal to one");
            }

            Subject = subject ?? throw new ArgumentNullException($"{nameof(Subject)} parameter cannot be null");
            Text = text ?? throw new ArgumentNullException($"{nameof(Text)} parameter cannot be null");

            // Optional parameters
            Html = html;
            if(attachments != null)
            {
                Attachments = DisposableStreamReaderList.FromList(attachments);
            }
        }
    }

    public class ParticipantInfo
    {
        public string Email { get; }
        public string Name { get; }

        public ParticipantInfo(string email, string name)
        {
            Email = email ?? throw new ArgumentNullException($"{nameof(Email)} parameter cannot be null");
            Name = name;
        }
    }
}
