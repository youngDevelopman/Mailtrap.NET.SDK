namespace Mailtrap.NET.SDK
{
    /// <summary>
    /// Defines sending options for the client
    /// Transactional types are best suited for time-sensitive emails. Triggered for a single recipient at once
    /// Bulk types are great for emails that are being sent to many recipients at once
    /// </summary>
    public enum SenderOptions
    {
        TransactionalHttp = 0,
        TransactionalSmtp = 1,
        BulkHttp = 2,
        BulkSmtp = 3,
    }
}
