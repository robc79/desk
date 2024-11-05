using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.ViewTaggedUserItems;

public class ViewTaggedUserItemsRequest : IRequest<List<SummaryItemDto>>
{
    public Guid UserId { get; }

    public int TagId { get; }

    public ViewTaggedUserItemsRequest(Guid userId, int tagId)
    {
        UserId = userId;
        TagId = tagId;
    }
}