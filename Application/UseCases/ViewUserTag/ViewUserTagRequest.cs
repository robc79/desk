using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.ViewTag;

public class ViewUserTagRequest : IRequest<TagDto?>
{
    public int TagId { get; }

    public Guid UserId { get; }

    public ViewUserTagRequest(int tagId, Guid userId)
    {
        TagId = tagId;
        UserId = userId;
    }
}