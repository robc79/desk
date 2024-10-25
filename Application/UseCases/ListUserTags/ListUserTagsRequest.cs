using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.ListUserTags;

public class ListUserTagsRequest : IRequest<List<TagDto>>
{
    public Guid UserId { get; }

    public ListUserTagsRequest(Guid userId)
    {
        UserId = userId;
    }
}