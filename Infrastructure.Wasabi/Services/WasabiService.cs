using Desk.Application.Configuration;
using Desk.Application.Services;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Desk.Infrastructure.Wasabi.Services;

public class WasabiService : IWasabiService
{
    private readonly WasabiConfiguration _config;

    public WasabiService(WasabiConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        // TODO: Setup AWS SDK for S3 bucket access using keys in config.
    }

    public async Task<string> UploadImageAsync(byte[] imageBytes, Guid ownerId, CancellationToken ct)
    {
        var assignedFilename = Guid.NewGuid();
        // TODO: Call out to Wasabi and upload the file.
        return assignedFilename.ToString();
    }
}