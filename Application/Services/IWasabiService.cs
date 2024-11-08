namespace Desk.Application.Services;

public interface IWasabiService
{
    Task<string> UploadImageAsync(byte[] imageBytes, Guid ownerId, CancellationToken ct);
}