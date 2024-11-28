using Desk.Application.Services;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.DeleteUserItem;

public class DeleteUserItemHandler : IRequestHandler<DeleteUserItemRequest, DeleteUserItemResponse>
{
    private readonly IItemRepository _itemRepository;
    
    private readonly IUnitOfWork _unitOfWork;

    private readonly IImageService _imageService;

    private readonly ILogger<DeleteUserItemHandler> _logger;

    public DeleteUserItemHandler(
        IItemRepository itemRepository,
        IUnitOfWork unitOfWork,
        IImageService imageService,
        ILogger<DeleteUserItemHandler> logger)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<DeleteUserItemResponse> Handle(DeleteUserItemRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete user item - {@request}", request);
        var item = await _itemRepository.GetByUserAndIdAsync(request.ItemId, request.UserId, cancellationToken);

        if (item is null)
        {
            return new DeleteUserItemResponse("Item not found.");
        }

        if (item.ImageName is not null)
        {
            _ = await _imageService.DeleteImageAsync(item.ImageName, cancellationToken);
            item.ImageName = null;
        }

        item.IsDeleted = true;
        string? errorMsg = null;

        try
        {
            await _unitOfWork.CommitChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to mark item '{item.Id}' as deleted.");
            errorMsg = ex.Message;
        }

        return new DeleteUserItemResponse(errorMsg);
    }
}
