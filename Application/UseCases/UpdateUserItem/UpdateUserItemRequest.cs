using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.UpdateUserItem;

public class UpdateUserItemRequest : IRequest<UpdateUserItemResponse>
{
    public Guid UserId { get; }

    public int ItemId { get; }

    public string Name { get; }

    public string? Description { get; }

    public ItemStatusEnum Status { get; }

    public ItemLocationEnum Location { get; }

    public int[] TagIds { get; }

    public UpdateUserItemRequest(
        Guid userId,
        int itemId,
        string name,
        string? description,
        ItemStatusEnum status,
        ItemLocationEnum location,
        int[] tagIds)
    {
        UserId = userId;
        ItemId = itemId;
        
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        Name = name;
        Description = description;
        Status = status;
        Location = location;
        TagIds = tagIds;
    }
}