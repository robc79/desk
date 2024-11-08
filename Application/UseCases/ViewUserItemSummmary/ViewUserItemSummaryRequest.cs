using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.ViewUserItemSummary;

public class ViewUserItemSummaryRequest : IRequest<SummaryItemDto?>
{
    public Guid UserId { get; }

    public int ItemId { get; }

    public ViewUserItemSummaryRequest(Guid userId, int itemId)
    {
        UserId = userId;
        ItemId = itemId;
    }
}