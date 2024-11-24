using Desk.Application.Dtos;
using Desk.Application.Mapping;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.ViewUserItems;

public class ViewUserItemsHandler : IRequestHandler<ViewUserItemsRequest, List<SummaryItemDto>>
{
    private readonly IItemRepository _itemRepository;

    private readonly ILogger<ViewUserItemsHandler> _logger;

    public ViewUserItemsHandler(IItemRepository itemRepository, ILogger<ViewUserItemsHandler> logger)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<SummaryItemDto>> Handle(ViewUserItemsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("View user items - {@request}", request);
        var mappedLocation = EnumMapping.MapToDomain(request.Location);
        var items = await _itemRepository.GetByUserAndLocationAsync(mappedLocation, request.UserId, cancellationToken);

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
