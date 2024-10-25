using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.AddUserTag;

public class AddUserTagHandler : IRequestHandler<AddUserTagRequest, AddUserTagResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITagRepository _tagRepository;
    private readonly IUserRepository _userRepository;

    public AddUserTagHandler(
        IUnitOfWork unitOfWork,
        ITagRepository tagRepository,
        IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<AddUserTagResponse> Handle(AddUserTagRequest request, CancellationToken cancellationToken)
    {
        var owner = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (owner is null)
        {
            return AddUserTagResponse.Failure("User not found!");
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
            // TODO: Log exception.
            saveFailed = true;
            failureReason = ex.Message;
        }

        return saveFailed ? AddUserTagResponse.Failure(failureReason) : AddUserTagResponse.Success(tag.Id);
    }
}