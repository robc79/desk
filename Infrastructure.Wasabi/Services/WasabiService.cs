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
    }

    public async Task<string> UploadImageAsync(byte[] imageBytes, Guid ownerId, CancellationToken ct)
    {
        var assignedFilename = Guid.NewGuid();

        return assignedFilename.ToString();
    }
}