using Desk.Application.Dtos;
using Desk.Application.Mapping;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.ViewUserItemSummary;

public class ViewUserItemSummaryHandler : IRequestHandler<ViewUserItemSummaryRequest, SummaryItemDto?>
{
    private readonly IItemRepository _itemRepository;

    private readonly ILogger<ViewUserItemSummaryHandler> _logger;

    public ViewUserItemSummaryHandler(IItemRepository itemRepository, ILogger<ViewUserItemSummaryHandler> logger)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<SummaryItemDto?> Handle(ViewUserItemSummaryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("View user item summary - {@request}", request);
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
