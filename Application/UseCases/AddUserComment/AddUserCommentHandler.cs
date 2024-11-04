using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.AddUserComment;

public class AddUserCommentHandler : IRequestHandler<AddUserCommentRequest, AddUserCommentResponse>
{
    private readonly IUserRepository _userRepository;

    public AddUserCommentHandler(IUserRepository userRepository)
    {    
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<AddUserCommentResponse> Handle(AddUserCommentRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return AddUserCommentResponse.Failure("User not found.");
        }

        return AddUserCommentResponse.Success(comment.Id);
    }
}
