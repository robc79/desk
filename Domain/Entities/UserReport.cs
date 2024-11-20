namespace Desk.Domain.Entities;

public class UserReport
{
    public string Username { get; }

    public int ItemCount { get; }

    public int ImageCount { get; }

    public UserReport(string username, int itemCount, int imageCount)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        ItemCount = itemCount;
        ImageCount = imageCount;
    }
}