namespace Mailtrap.NET.SDK.Exceptions
{
    /// <summary>
    /// Thrown when the sender is not registred or not supported
    /// </summary>
    public class EmailSendingException : Exception
    {
        public EmailSendingException()
        {
        }

        public EmailSendingException(string message)
            : base(message)
        {
        }

        public EmailSendingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
