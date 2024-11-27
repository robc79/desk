namespace Desk.Domain.Entities;

public class UserAuditEntry
{
    public int Id { get; protected set; }

    public Guid UserId { get; protected set; }

    public User User { get; protected set; }

    public string EventType { get; protected set; }

    public DateTimeOffset CreatedOn { get; protected set; }

    public DateTimeOffset? UpdatedOn { get; protected set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    
    protected UserAuditEntry()
    {
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    
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