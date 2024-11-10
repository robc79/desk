namespace Desk.Application.Services;

public interface IWasabiService
{
    Task<string?> UploadImageAsync(byte[] imageBytes, Guid ownerId, CancellationToken ct);

    Task<byte[]?> DownloadImageAsync(string key, CancellationToken ct);

    Task<bool> DeleteImageAsync(string key, CancellationToken ct);
}