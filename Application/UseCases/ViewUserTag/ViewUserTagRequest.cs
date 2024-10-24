using Desk.Application.Dtos;
using MediatR;

namespace Desk.Application.UseCases.ViewTag;

public class ViewUserTagRequest : IRequest<TagDto?>
{
    public int TagId { get; }

    public int UserId { get; }

    public ViewUserTagRequest(int tagId, int userId)
    {
        TagId = tagId;
        UserId = userId;
    }
}