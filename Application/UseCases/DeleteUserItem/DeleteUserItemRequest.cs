using MediatR;

namespace Desk.Application.UseCases.DeleteUserItem;

public class DeleteUserItemRequest : IRequest<DeleteUserItemResponse>
{
    public Guid UserId { get; }

    public int ItemId { get; }

    public DeleteUserItemRequest(Guid userId, int itemId)
    {
        UserId = userId;
        ItemId = itemId;
    }
}