using Desk.Domain.Repositories;
using MediatR;

namespace Desk.Application.UseCases.DeleteUserTag;

public class DeleteUserTagHandler : IRequestHandler<DeleteUserTagRequest, DeleteUserTagResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITagRepository _tagRepository;

    public DeleteUserTagHandler(IUnitOfWork unitOfWork, ITagRepository tagRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    }

    public async Task<DeleteUserTagResponse> Handle(DeleteUserTagRequest request, CancellationToken cancellationToken)
    {
        var response = new DeleteUserTagResponse();
        var tag = await _tagRepository.GetByUserAndIdAsync(request.TagId, request.UserId, cancellationToken);

        if (tag is null)
        {
            return response;
        }

        await _tagRepository.DeleteAsync(tag.Id, cancellationToken);

        try
        {
            await _unitOfWork.CommitChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // TODO: Log exception.
            response = new DeleteUserTagResponse(ex.Message);
        }

        return response;
    }
}
