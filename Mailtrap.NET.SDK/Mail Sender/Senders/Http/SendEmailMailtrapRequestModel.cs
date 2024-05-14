namespace Mailtrap.NET.SDK.Mail_Sender.Senders.Http
{
    internal class SendEmailMailtrapRequestModel
    {
        public ParticipantInfo From { get; }

        public IReadOnlyCollection<ParticipantInfo> To { get; }

        public string Subject { get; }

        public string Text { get; }

        public string Html { get; }

        public IReadOnlyCollection<Attachment> Attachments { get; set; }

        public SendEmailMailtrapRequestModel(
            ParticipantInfo from,
            IReadOnlyCollection<ParticipantInfo> to,
            string subject,
            string text,
            string html,
            IReadOnlyCollection<Attachment> attachments)
        {
            // Required parameters
            From = from ?? throw new ArgumentNullException($"{nameof(From)} parameter cannot be null");

            To = to ?? throw new ArgumentNullException($"{nameof(To)} parameter cannot be null");
            if (To.Count <= 0)
            {
                throw new ArgumentException($"The amount of recepients has to be more or equal to one");
            }

            Subject = subject ?? throw new ArgumentNullException($"{nameof(Subject)} parameter cannot be null");
            Text = text ?? throw new ArgumentNullException($"{nameof(Text)} parameter cannot be null");

            // Optional parameters
            Html = html;
            Attachments = attachments;
        }

        internal class ParticipantInfo
        {
            public string Email { get; }
            public string Name { get; }

            public ParticipantInfo(string email, string name)
            {
                Email = email ?? throw new ArgumentNullException($"{nameof(Email)} parameter cannot be null");
                Name = name;
            }
        }

        internal class Attachment
        {
            public string Content { get; }

            public string FileName { get; }

            public string Type { get; }

            public string Disposition { get; } = "Attachment";

            public Attachment(string content, string fileName, string type)
            {
                Content = content ?? throw new ArgumentNullException($"{nameof(Content)} parameter cannot be null");
                FileName = fileName ?? throw new ArgumentNullException($"{nameof(FileName)} parameter cannot be null");
                Type = type ?? throw new ArgumentNullException($"{nameof(Type)} parameter cannot be null");
            }
        }
    }
}
