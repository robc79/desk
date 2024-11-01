using MediatR;

namespace Desk.Application.UseCases.UpdateUserItemDescription;

public class UpdateUserItemDescriptionRequest : IRequest<UpdateUserItemDescriptionResponse>
{
    public Guid UserId { get; }

    public int ItemId { get; }

    public string? Description { get; }

    public UpdateUserItemDescriptionRequest(
        Guid userId,
        int itemId,
        string? description)
    {
        UserId = userId;
        ItemId = itemId;
        Description = description;
    }
}