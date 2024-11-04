using Desk.Application.Dtos;
using Desk.Application.Mapping;
using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.ViewUserItems;

public class ViewUserItemsHandler : IRequestHandler<ViewUserItemsRequest, List<SummaryItemDto>>
{
    private readonly IItemRepository _itemRepository;

    public ViewUserItemsHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public async Task<List<SummaryItemDto>> Handle(ViewUserItemsRequest request, CancellationToken cancellationToken)
    {
        var mappedLocation = EnumMapping.MapToDomain(request.Location);
        var items = await _itemRepository.GetByUserAndLocation(mappedLocation, request.UserId, cancellationToken);

        return items.Select(i => new SummaryItemDto
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            Location = EnumMapping.MapFromDomain(i.Location),
            CurrentStatus = EnumMapping.MapFromDomain(i.CurrentStatus),
            Tags = i.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToArray()
        }).ToList();
    }
}
