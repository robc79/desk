namespace Desk.Domain.Entities;

public class UserAuditEntry
{
    public int Id { get; protected set; }

    public Guid UserId { get; protected set; }

    public User User { get; protected set; }

    public string EventType { get; protected set; }

    public DateTimeOffset CreatedOn { get; protected set; }

    public DateTimeOffset? UpdatedOn { get; protected set; }

    public UserAuditEntry(User user, string eventType)
    {
        User = user;

        if (String.IsNullOrWhiteSpace(eventType))
        {
            throw new ArgumentException("Event type must be supplied.", nameof(eventType));
        }

        EventType = eventType;
    }
}