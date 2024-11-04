namespace Desk.Application.Dtos;

public class TextCommentDto
{
    public int Id { get; set; }

    public string? Comment { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
}