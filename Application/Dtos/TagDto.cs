namespace Desk.Application.Dtos;

public class TagDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public UserDto? Owner { get; set; }
}