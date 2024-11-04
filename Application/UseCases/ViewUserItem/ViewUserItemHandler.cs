using Desk.Application.Dtos;
using Desk.Application.Mapping;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.Features;

namespace Desk.Application.UseCases.ViewUserItem;

public class ViewUserItemHandler : IRequestHandler<ViewUserItemRequest, FullItemDto?>
{
    private readonly IItemRepository _itemRepository;

    public ViewUserItemHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public async Task<FullItemDto?> Handle(ViewUserItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetWithCommentsByUserAndIdAsync(request.ItemId, request.UserId, cancellationToken);

        if (item is null)
        {
            return null;
        }

        return new FullItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Tags = item.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToArray(),
            TextComments = item.TextComments.Select(c => new TextCommentDto { Id = c.Id, Comment = c.Comment, CreatedOn = c.CreatedOn }).ToArray(),
            Location = EnumMapping.MapFromDomain(item.Location),
            CurrentStatus = EnumMapping.MapFromDomain(item.CurrentStatus)
        };
    }
}
