namespace Desk.Application.Dtos;

public class UserReportDto
{
    public Guid Id { get; set; }

    public string? Username { get; set; }

    public int ItemCount { get; set; }

    public int ImageCount { get; set; }
}