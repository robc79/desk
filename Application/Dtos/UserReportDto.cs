namespace Desk.Application.Dtos;

public class UserReportDto
{
    public Guid UserId { get; set; }

    public string? Username { get; set; }

    public bool IsConfirmed { get; set; }

    public DateTimeOffset? LastLoginOn { get; set; }
}