namespace Desk.Domain.Entities;

public class UserReport
{
    public Guid UserId { get; }

    public string Username { get; }

    public bool IsConfirmed { get; }

    public DateTimeOffset? LastLoginOn { get; }

    public UserReport(
        Guid userId,
        string username,
        bool isConfirmed,
        DateTimeOffset? lastLoginOn)
    {
        UserId = userId;
        Username = username;
        IsConfirmed = isConfirmed;
        LastLoginOn = lastLoginOn;
    }
}