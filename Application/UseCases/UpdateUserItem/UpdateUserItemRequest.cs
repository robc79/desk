using MediatR;

namespace Desk.Application.UseCases.UpdateUserItem;

public class UpdateUserItemRequest : IRequest<UpdateUserItemResponse>
{
    public Guid UserId { get; }

    public int ItemId { get; }

    public string? Description { get; }

    public UpdateUserItemRequest(
        Guid userId,
        int itemId,
        string? description)
    {
        UserId = userId;
        ItemId = itemId;
        Description = description;
    }
}