using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.UpdateUserItem;

public class UpdateUserItemHandler : IRequestHandler<UpdateUserItemRequest, UpdateUserItemResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IItemRepository _itemRepository;

    private ILogger<UpdateUserItemHandler> _logger;

    public UpdateUserItemHandler(
        ILogger<UpdateUserItemHandler> logger,
        IUnitOfWork unitOfWork,
        IItemRepository itemRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public async Task<UpdateUserItemResponse> Handle(UpdateUserItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByUserAndIdAsync(request.ItemId, request.UserId, cancellationToken);

        if (item is null)
        {
            return new UpdateUserItemResponse("Item not found.");
        }

        item.Description = request.Description ?? string.Empty;
        
        string? error = null;

        try
        {
            await _unitOfWork.CommitChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update item - {@request}.", request);
            error = ex.Message;
        }

        return new UpdateUserItemResponse(error);
    }
}
