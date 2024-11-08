using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.AddUserItem;

public class AddUserItemRequest : IRequest<AddUserItemResponse>
{
    public Guid UserId { get; }

    public string Name { get; }

    public byte[]? ImageBytes { get; }

    public string? Description { get; }

    public ItemLocationEnum Location { get; }

    public ItemStatusEnum Status { get; }

    public int[]? TagIds { get; }

    public AddUserItemRequest(
        Guid userId,
        string name,
        byte[]? imageBytes,
        string? description,
        ItemLocationEnum location,
        ItemStatusEnum status,
        int[]? tagIds)
    {
        UserId = userId;
        Name = name;
        ImageBytes = imageBytes;
        Description = description;
        Location = location;
        Status = status;
        TagIds = tagIds;
    }
}