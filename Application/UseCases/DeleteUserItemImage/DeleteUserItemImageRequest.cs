using MediatR;

namespace Desk.Application.UseCases.DeleteUserItemImage;

public class DeleteUserItemImageRequest : IRequest<DeleteUserItemImageResponse>
{
    public Guid UserId { get; }

    public int ItemId { get; }

    public DeleteUserItemImageRequest(Guid userId, int itemId)
    {
        UserId = userId;
        ItemId = itemId;
    }
}