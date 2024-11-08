using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Desk.Application.Configuration;
using Desk.Application.Services;

namespace Desk.Infrastructure.Wasabi.Services;

public class WasabiService : IWasabiService
{
    private readonly WasabiConfiguration _config;
    private readonly AmazonS3Config _s3Config;

    public WasabiService(WasabiConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        AWSConfigs.AWSRegion = _config.Region;
        _s3Config = new AmazonS3Config { ServiceURL = "https://s3.wasabisys.com" };
    }

    public async Task<byte[]> DownloadImageAsync(string key, CancellationToken ct)
    {
        byte[] imageBytes;

        using (var s3 = new AmazonS3Client(_config.Id, _config.Secret, _s3Config))
        {
            var getRequest = new GetObjectRequest
            {
                BucketName = _config.BucketName,
                Key = key
            };

            var response = await s3.GetObjectAsync(getRequest, ct);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                // TODO: Log the error and propagate a response (throw application level exception).
                throw new Exception("Wasabi exception coming soon.");
            }
            
            using (var ms = new MemoryStream())
            {
                await response.ResponseStream.CopyToAsync(ms, ct);
                imageBytes = ms.ToArray();
            }
        }
        
        return imageBytes;
    }

    public async Task<string> UploadImageAsync(byte[] imageBytes, Guid ownerId, CancellationToken ct)
    {
        var assignedFilename = $"{ownerId}/{Guid.NewGuid()}";

        using (var s3 = new AmazonS3Client(_config.Id, _config.Secret, _s3Config))
        using (var ms = new MemoryStream(imageBytes))
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _config.BucketName,
                Key = assignedFilename,
                InputStream = ms
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