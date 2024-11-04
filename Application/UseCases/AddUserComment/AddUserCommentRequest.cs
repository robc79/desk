using MediatR;

namespace Desk.Application.UseCases.AddUserComment;

public class AddUserCommentRequest : IRequest<AddUserCommentResponse>
{
    public Guid UserId { get; }

    public int ItemId { get; }

    public string Comment { get; }

    public AddUserCommentRequest(Guid userId, int itemId, string comment)
    {
        UserId = userId;
        ItemId = itemId;

        if (String.IsNullOrWhiteSpace(comment))
        {
            throw new ArgumentException("Comment is required.", nameof(comment));
        }

        Comment = comment;
    }
}