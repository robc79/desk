using Desk.Application.Dtos;
using Desk.Application.Mapping;
using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.ViewUserItemSummary;

public class ViewUserItemSummaryHandler : IRequestHandler<ViewUserItemSummaryRequest, SummaryItemDto?>
{
    private readonly IItemRepository _itemRepository;

    public ViewUserItemSummaryHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public async Task<SummaryItemDto?> Handle(ViewUserItemSummaryRequest request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByUserAndIdAsync(request.ItemId, request.UserId, cancellationToken);

        if (item == null)
        {
            return null;
        }
        
        return new SummaryItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            CurrentStatus = EnumMapping.MapFromDomain(item.CurrentStatus),
            Location = EnumMapping.MapFromDomain(item.Location),
            Tags = item.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToArray()
        };
    }
}
