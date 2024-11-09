using MediatR;

namespace Desk.Application.UseCases.UpdateUserItemImage;

public class UpdateUserItemImageRequest : IRequest<UpdateUserItemImageResponse>
{
    public Guid UserId { get; set; }

    public int ItemId { get; set; }

    public byte[] ImageBytes { get; set; }

    public UpdateUserItemImageRequest(Guid userId, int itemId, byte[] imageBytes)
    {
        UserId = userId;
        ItemId = itemId;
        ImageBytes = imageBytes;
    }
}