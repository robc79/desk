using MediatR;

namespace Desk.Application.UseCases.DeleteUserTag;

public class DeleteUserTagRequest : IRequest<DeleteUserTagResponse>
{
    public int TagId { get; }

    public Guid UserId { get; }

    public DeleteUserTagRequest(int tagId, Guid userId)
    {
        TagId = tagId;
        UserId = userId;
    }
}