namespace Mailtrap.NET.SDK.Mail_Sender.Senders.Http
{
    internal class SendEmailMailtrapRequestModel
    {
        // Required
        public ParticipantInfo From { get; }

        public IReadOnlyCollection<ParticipantInfo> To { get; }

        public string Subject { get; }

        public string Text { get; }

        //Optional
        public string Html { get; init; }

        public IReadOnlyCollection<Attachment> Attachments { get; init; }

        public SendEmailMailtrapRequestModel(
            ParticipantInfo from,
            IReadOnlyCollection<ParticipantInfo> to,
            string subject,
            string text)
        {
            From = from ?? throw new ArgumentNullException($"{nameof(From)} parameter cannot be null");

            To = to ?? throw new ArgumentNullException($"{nameof(To)} parameter cannot be null");
            if (To.Count <= 0)
            {
                throw new ArgumentException($"The amount of recepients has to be more or equal to one");
            }

            Subject = subject ?? throw new ArgumentNullException($"{nameof(Subject)} parameter cannot be null");
            Text = text ?? throw new ArgumentNullException($"{nameof(Text)} parameter cannot be null");
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

            public string Disposition { get; } = "attachment";

            public Attachment(string content, string fileName, string type)
            {
                Content = content ?? throw new ArgumentNullException($"{nameof(Content)} parameter cannot be null");
                FileName = fileName ?? throw new ArgumentNullException($"{nameof(FileName)} parameter cannot be null");
                Type = type ?? throw new ArgumentNullException($"{nameof(Type)} parameter cannot be null");
            }
        }
    }
}
