using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.AddUserComment;

public class AddUserCommentHandler : IRequestHandler<AddUserCommentRequest, AddUserCommentResponse>
{
    private IUnitOfWork _unitOfWork;

    private ILogger<AddUserCommentHandler> _logger;

    private readonly IUserRepository _userRepository;

    private readonly IItemRepository _itemRepository;

    public AddUserCommentHandler(
        IUnitOfWork unitOfWork,
        ILogger<AddUserCommentHandler> logger,
        IUserRepository userRepository,
        IItemRepository itemRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public async Task<AddUserCommentResponse> Handle(AddUserCommentRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Add user comment - {@request}", request);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return AddUserCommentResponse.Failure("User not found.");
        }

        var item = await _itemRepository.GetByUserAndIdAsync(request.ItemId, user.Id, cancellationToken);

        if (item is null)
        {
            return AddUserCommentResponse.Failure("Item not found.");
        }
        
        var comment = new TextComment(item, request.Comment);
        item.TextComments.Add(comment);
        
        string? error = null;

        try
        {
            await _unitOfWork.CommitChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add new comment - {@request}.", request);
            error = ex.Message;
        }

        return error is not null ? AddUserCommentResponse.Failure(error) : AddUserCommentResponse.Success(comment.Id);
    }
}
