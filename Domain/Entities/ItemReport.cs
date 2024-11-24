namespace Desk.Domain.Entities;

public class ItemReport
{
    public string Username { get; }

    public int ItemCount { get; }

    public int DeletedCount { get; }

    public int ImageCount { get; }

    public ItemReport(string username, int itemCount, int deletedCount, int imageCount)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        ItemCount = itemCount;
        DeletedCount = deletedCount;
        ImageCount = imageCount;
    }
}