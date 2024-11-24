using Desk.Application.Dtos;
using Desk.Application.Mapping;
using Desk.Application.Services;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.ViewUserItem;

public class ViewUserItemHandler : IRequestHandler<ViewUserItemRequest, FullItemDto?>
{
    private readonly IItemRepository _itemRepository;

    private readonly IImageService _imageService;

    private readonly ILogger<ViewUserItemHandler> _logger;

    public ViewUserItemHandler(
        IItemRepository itemRepository,
        IImageService imageService,
        ILogger<ViewUserItemHandler> logger)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<FullItemDto?> Handle(ViewUserItemRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("View user item - {@request}", request);
        var item = await _itemRepository.GetWithCommentsByUserAndIdAsync(request.ItemId, request.UserId, cancellationToken);

        if (item is null)
        {
            return null;
        }

        byte[]? imageBytes = null;

        if (item.ImageName is not null)
        {
            imageBytes = await _imageService.DownloadImageAsync(item.ImageName, cancellationToken);
        }

        return new FullItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Tags = item.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToArray(),
            TextComments = item.TextComments.Select(c => new TextCommentDto { Id = c.Id, Comment = c.Comment, CreatedOn = c.CreatedOn }).ToArray(),
            Location = EnumMapping.MapFromDomain(item.Location),
            CurrentStatus = EnumMapping.MapFromDomain(item.CurrentStatus),
            ImageBytes = imageBytes
        };
    }
}
