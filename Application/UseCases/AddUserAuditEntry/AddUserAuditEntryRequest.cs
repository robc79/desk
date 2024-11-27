using MediatR;

namespace Desk.Application.UseCases.AddUserAuditEntry;

public class AddUserAuditEntryRequest : IRequest
{
    public Guid UserId { get; }

    public string EventType { get; }

    public AddUserAuditEntryRequest(Guid userId, string eventType)
    {
        UserId = userId;

        if (String.IsNullOrWhiteSpace(eventType))
        {
            throw new ArgumentException("Event type must be supplied.", nameof(eventType));
        }
        
        EventType = eventType;
    }
}