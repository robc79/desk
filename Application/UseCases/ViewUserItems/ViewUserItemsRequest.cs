using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.ViewUserItems;

public class ViewUserItemsRequest : IRequest<List<SummaryItemDto>>
{
    public Guid UserId { get; }

    public ItemLocationEnum Location { get; }

    public ViewUserItemsRequest(Guid userId, ItemLocationEnum location)
    {
        UserId = userId;
        Location = location;
    }
}