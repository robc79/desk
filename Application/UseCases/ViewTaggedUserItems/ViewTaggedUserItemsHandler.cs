using System.Runtime.CompilerServices;
using Desk.Application.Dtos;
using Desk.Application.Mapping;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.ViewTaggedUserItems;

public class ViewTaggedUserItemsHandler : IRequestHandler<ViewTaggedUserItemsRequest, List<SummaryItemDto>>
{
    private readonly IItemRepository _itemRepository;

    private readonly ILogger<ViewTaggedUserItemsHandler> _logger;

    public ViewTaggedUserItemsHandler(IItemRepository itemRepository, ILogger<ViewTaggedUserItemsHandler> logger)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<SummaryItemDto>> Handle(ViewTaggedUserItemsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("View tagged user items - {@request}", request);
        var items = await _itemRepository.GetByUserAndTagAsync(request.TagId, request.UserId, cancellationToken);
        
        return items
        .Select(i => new SummaryItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Location = EnumMapping.MapFromDomain(i.Location),
                CurrentStatus = EnumMapping.MapFromDomain(i.CurrentStatus),
                Tags = i.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToArray()
            })
        .ToList();
    }
}
