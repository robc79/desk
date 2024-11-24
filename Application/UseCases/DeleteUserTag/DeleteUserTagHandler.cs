using Desk.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Desk.Application.UseCases.DeleteUserTag;

public class DeleteUserTagHandler : IRequestHandler<DeleteUserTagRequest, DeleteUserTagResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly ITagRepository _tagRepository;

    private readonly ILogger<DeleteUserTagHandler> _logger;

    public DeleteUserTagHandler(
        ILogger<DeleteUserTagHandler> logger,
        IUnitOfWork unitOfWork,
        ITagRepository tagRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    }

    public async Task<DeleteUserTagResponse> Handle(DeleteUserTagRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete user tag - {@request}", request);
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
            _logger.LogError(ex, "Failed to delete tag - {@request}.", request);
            response = new DeleteUserTagResponse(ex.Message);
        }

        return response;
    }
}
