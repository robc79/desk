using Desk.Application.Mapping;
using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.UpdateUserItem;

public class UpdateUserItemHandler : IRequestHandler<UpdateUserItemRequest, UpdateUserItemResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IItemRepository _itemRepository;

    private readonly ITagRepository _tagRepository;

    private readonly IUserRepository _userRepository;

    private ILogger<UpdateUserItemHandler> _logger;

    public UpdateUserItemHandler(
        ILogger<UpdateUserItemHandler> logger,
        IUnitOfWork unitOfWork,
        IItemRepository itemRepository,
        ITagRepository tagRepository,
        IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<UpdateUserItemResponse> Handle(UpdateUserItemRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update user item - {@request}", request);
        var owner = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (owner is null)
        {
            return new UpdateUserItemResponse("User not found.");
        }

        var item = await _itemRepository.GetByUserAndIdAsync(request.ItemId, request.UserId, cancellationToken);

        if (item is null)
        {
            return new UpdateUserItemResponse("Item not found.");
        }

        item.Name = request.Name;
        item.Description = request.Description ?? string.Empty;
        item.CurrentStatus = EnumMapping.MapToDomain(request.Status);
        item.Location = EnumMapping.MapToDomain(request.Location);
        
        var tagsToApply = await LoadTagsAsync(request.TagIds, request.UserId, cancellationToken);

        if (request.TagIds is not null)
        {
            tagsToApply = await LoadTagsAsync(request.TagIds, owner.Id, cancellationToken);    
        
            if (tagsToApply.Count != request.TagIds.Length)
            {
                return new UpdateUserItemResponse("Could not find all selected tags.");
            }
        }

        item.Tags.Clear();
        
        foreach (var tag in tagsToApply)
        {
            item.Tags.Add(tag);
        }

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

    private async Task<List<Tag>> LoadTagsAsync(int[] tagIds, Guid userId, CancellationToken ct)
    {
        var tags = new List<Tag>();

        foreach (var tagId in tagIds)
        {
            var tag = await _tagRepository.GetByUserAndIdAsync(tagId, userId, ct);

            if (tag is not null)
            {
                tags.Add(tag);
            }
        }

        return tags;
    }
}
