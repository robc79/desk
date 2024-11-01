using Desk.Application.Dtos;
using Desk.Domain.Entities;
using MediatR;

namespace Desk.Application.UseCases.ViewUserItem;

public class ViewUserItemRequest : IRequest<FullItemDto?>
{
    public Guid UserId { get; }

    public int ItemId { get; }

    public ViewUserItemRequest(Guid userId, int itemId)
    {
        UserId = userId;
        ItemId = itemId;
    }
}