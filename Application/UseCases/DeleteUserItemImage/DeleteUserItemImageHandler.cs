using Desk.Application.Services;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.DeleteUserItemImage;

public class DeleteUserItemImageHandler : IRequestHandler<DeleteUserItemImageRequest, DeleteUserItemImageResponse>
{
    private readonly ILogger<DeleteUserItemImageHandler> _logger;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IItemRepository _itemRepository;

    private readonly IImageService _wasabiService;

    public DeleteUserItemImageHandler(
        ILogger<DeleteUserItemImageHandler> logger,
        IUnitOfWork unitOfWork,
        IImageService wasabiService,
        IItemRepository itemRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _wasabiService = wasabiService ?? throw new ArgumentNullException(nameof(wasabiService));
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public async Task<DeleteUserItemImageResponse> Handle(DeleteUserItemImageRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete user item image - {@request}", request);
        var item = await _itemRepository.GetByUserAndIdAsync(request.ItemId, request.UserId, cancellationToken);

        if (item is null)
        {
            return new DeleteUserItemImageResponse("Item not found.");
        }

        if (item.ImageName is not null)
        {
            var currentFilename = item.ImageName;
            var deleteSucceeded = await _wasabiService.DeleteImageAsync(currentFilename, cancellationToken);
            
            if (!deleteSucceeded)
            {
                return new DeleteUserItemImageResponse("Unable to delete image.");
            }

            item.ImageName = null;
        }
        
        string? error = null;

        try
        {
            await _unitOfWork.CommitChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update item image - {@request}.", request);
            error = ex.Message;
        }

        return new DeleteUserItemImageResponse(error);
    }
}
