namespace Desk.Application.Dtos;

public class SummaryItemDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public TagDto[] Tags { get; set; } = [];
}