using Desk.Application.Mapping;
using Desk.Application.Services;
using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.AddUserItem;

public class AddUserItemHandler : IRequestHandler<AddUserItemRequest, AddUserItemResponse>
{
    private readonly ILogger<AddUserItemHandler> _logger;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ITagRepository _tagRepository;
    
    private readonly IItemRepository _itemRepository;

    private readonly IUserRepository _userRepository;

    private readonly IImageService _wasabiService;

    public AddUserItemHandler(
        ILogger<AddUserItemHandler> logger,
        IUnitOfWork unitOfWork,
        ITagRepository tagRepository,
        IItemRepository itemRepository,
        IUserRepository userRepository,
        IImageService wasabiService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _wasabiService = wasabiService ?? throw new ArgumentNullException(nameof(wasabiService));
    }

    public async Task<AddUserItemResponse> Handle(AddUserItemRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Add user item - {@request}", request);
        var mappedLocation = EnumMapping.MapToDomain(request.Location);
        var mappedStatus = EnumMapping.MapToDomain(request.Status);
        
        var owner = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (owner is null)
        {
            return AddUserItemResponse.Failure("Could not find user.");
        }
        
        List<Tag> tagsToApply = new List<Tag>();

        if (request.TagIds is not null)
        {
            tagsToApply = await LoadTagsAsync(request.TagIds, owner.Id, cancellationToken);    
        
            if (tagsToApply.Count != request.TagIds.Length)
            {
                return AddUserItemResponse.Failure("Could not find all selected tags.");
            }
        }
        
        var item = new Item(owner, mappedStatus, request.Name)
        {
            Description = request.Description is not null ? request.Description : string.Empty,
            Location = mappedLocation
        };

        foreach (var tag in tagsToApply)
        {
            item.Tags.Add(tag);
        }
        
        if (request.ImageBytes is not null)
        {
            var filename = await _wasabiService.UploadImageAsync(request.ImageBytes, owner.Id, cancellationToken);
            
            if (filename is null)
            {
                return AddUserItemResponse.Failure("Failed to upload image.");
            }

            item.ImageName = filename;
        }

        var error = string.Empty;

        try
        {
            await _itemRepository.AddAsync(item, cancellationToken);
            await _unitOfWork.CommitChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add new item - {@request}.", request);
            error = ex.Message;
        }

        return error == string.Empty ? AddUserItemResponse.Success(item.Id) : AddUserItemResponse.Failure(error);
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
