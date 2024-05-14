using Mailtrap.NET.SDK.DataStructures;

namespace Mailtrap.NET.SDK.Models
{
    public class SendEmailRequest
    {
        // Required
        public ParticipantInfo From { get; }

        public IReadOnlyCollection<ParticipantInfo> To { get; }

        public string Subject { get; }

        public string Text { get; }

        // Optional

        public string Html { get; init; }

        public DisposableStreamReaderList Attachments { get; init; } = new DisposableStreamReaderList();

        public SendEmailRequest(
            ParticipantInfo from, 
            IReadOnlyCollection<ParticipantInfo> to, 
            string subject, 
            string text)
        {
            From = from ?? throw new ArgumentNullException($"{nameof(From)} parameter cannot be null");

            To = to ?? throw new ArgumentNullException($"{nameof(To)} parameter cannot be null");
            if(To.Count <= 0) 
            {
                throw new ArgumentException($"The amount of recepients has to be more or equal to one");
            }

            Subject = subject ?? throw new ArgumentNullException($"{nameof(Subject)} parameter cannot be null");
            Text = text ?? throw new ArgumentNullException($"{nameof(Text)} parameter cannot be null");
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
