namespace Mailtrap.NET.SDK.Exceptions
{
    /// <summary>
    /// Thrown when the sender is not registred or not supported
    /// </summary>
    public class SenderNotSupportedException : Exception
    {
        public SenderNotSupportedException()
        {
        }

        public SenderNotSupportedException(string message)
            : base(message)
        {
        }

        public SenderNotSupportedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
