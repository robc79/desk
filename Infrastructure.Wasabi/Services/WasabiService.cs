using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Desk.Application.Configuration;
using Desk.Application.Services;

namespace Desk.Infrastructure.Wasabi.Services;

public class WasabiService : IWasabiService
{
    private readonly WasabiConfiguration _config;

    public WasabiService(WasabiConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        AWSConfigs.AWSRegion = _config.Region;
    }

    public async Task<string> UploadImageAsync(byte[] imageBytes, Guid ownerId, CancellationToken ct)
    {
        var assignedFilename = Guid.NewGuid().ToString();

        using (var s3 = new AmazonS3Client(_config.Id, _config.Secret))
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _config.BucketName,
                FilePath = ownerId.ToString(),
                Key = assignedFilename
            };

            var response = await s3.PutObjectAsync(putRequest, ct);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                // TODO: Log the error and propagate a response (throw application level exception).
            }
        }
             
        return assignedFilename;
    }
}