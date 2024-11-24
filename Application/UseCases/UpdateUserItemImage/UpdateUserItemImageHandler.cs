using Desk.Application.Services;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.UpdateUserItemImage;

public class UpdateUserItemImageHandler : IRequestHandler<UpdateUserItemImageRequest, UpdateUserItemImageResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IItemRepository _itemRepository;

    private readonly IImageService _wasabiService;

    private readonly ILogger<UpdateUserItemImageHandler> _logger;

    public UpdateUserItemImageHandler(
        ILogger<UpdateUserItemImageHandler> logger,
        IUnitOfWork unitOfWork,
        IItemRepository itemRepository,
        IImageService wasabiService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _wasabiService = wasabiService ?? throw new ArgumentNullException(nameof(wasabiService));
    }
    
    public async Task<UpdateUserItemImageResponse> Handle(UpdateUserItemImageRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update user item image - {@request}", request);
        var item = await _itemRepository.GetByUserAndIdAsync(request.ItemId, request.UserId, cancellationToken);

        if (item is null)
        {
            return new UpdateUserItemImageResponse("Item not found.");
        }

        if (item.ImageName is not null)
        {
            var currentFilename = item.ImageName;
            var deleteSucceeded = await _wasabiService.DeleteImageAsync(currentFilename, cancellationToken);
            
            if (!deleteSucceeded)
            {
                return new UpdateUserItemImageResponse("Unable to delete image.");
            }
        }

        var updatedFilename = await _wasabiService.UploadImageAsync(request.ImageBytes, request.UserId, cancellationToken);
        
        if (updatedFilename is null)
        {
            return new UpdateUserItemImageResponse("Unable to upload image.");
        }

        item.ImageName = updatedFilename;

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

        return new UpdateUserItemImageResponse(error);
    }
}
