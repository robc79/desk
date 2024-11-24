using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.AddUserTag;

public class AddUserTagHandler : IRequestHandler<AddUserTagRequest, AddUserTagResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ITagRepository _tagRepository;
    
    private readonly IUserRepository _userRepository;

    private readonly ILogger<AddUserTagHandler> _logger;

    public AddUserTagHandler(
        ILogger<AddUserTagHandler> logger,
        IUnitOfWork unitOfWork,
        ITagRepository tagRepository,
        IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<AddUserTagResponse> Handle(AddUserTagRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Add user tag - {@request}", request);
        var owner = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (owner is null)
        {
            return AddUserTagResponse.Failure("User not found!");
        }

        var existingTags = await _tagRepository.GetByUserAsync(request.UserId, cancellationToken);

        if (existingTags.Any(t => t.Name == request.Name))
        {
            var matchingTag = existingTags.First(t => t.Name == request.Name);

            return AddUserTagResponse.Success(matchingTag.Id);
        }
        
        var tag = new Tag(owner, request.Name);
        await _tagRepository.AddAsync(tag, cancellationToken);

        var saveFailed = false;
        string failureReason = "";

        try
        {
            await _unitOfWork.CommitChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add new tag - {@request}.", request);
            saveFailed = true;
            failureReason = ex.Message;
        }

        return saveFailed ? AddUserTagResponse.Failure(failureReason) : AddUserTagResponse.Success(tag.Id);
    }
}