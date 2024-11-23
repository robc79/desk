using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Desk.Application.Configuration;
using Desk.Application.Services;
using Microsoft.Extensions.Logging;

namespace Desk.Infrastructure.Wasabi.Services;

public class WasabiService : IImageService
{
    private readonly WasabiConfiguration _config;
    
    private readonly AmazonS3Config _s3Config;

    private ILogger<WasabiService> _logger;

    public WasabiService(ILogger<WasabiService> logger, WasabiConfiguration config)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _config = config ?? throw new ArgumentNullException(nameof(config));
        AWSConfigs.AWSRegion = _config.Region;
        _s3Config = new AmazonS3Config { ServiceURL = "https://s3.wasabisys.com" };
    }

    public async Task<bool> DeleteImageAsync(string key, CancellationToken ct)
    {
        using (var s3 = new AmazonS3Client(_config.Id, _config.Secret, _s3Config))
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _config.BucketName,
                Key = key
            };

            try
            {
                var response = await s3.DeleteObjectAsync(deleteRequest, ct);

                if (response.HttpStatusCode != HttpStatusCode.NoContent)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Wasabi failed on delete operation for '{key}'.");

                return false;
            }
        }

        return true;
    }

    public async Task<byte[]?> DownloadImageAsync(string key, CancellationToken ct)
    {
        byte[] imageBytes;

        using (var s3 = new AmazonS3Client(_config.Id, _config.Secret, _s3Config))
        {
            var getRequest = new GetObjectRequest
            {
                BucketName = _config.BucketName,
                Key = key
            };
            
            GetObjectResponse response;

            try
            {
                response = await s3.GetObjectAsync(getRequest, ct);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Wasabi failed on get operation for '{key}'.");

                return null;
            }

            using (var ms = new MemoryStream())
            {
                await response.ResponseStream.CopyToAsync(ms, ct);
                imageBytes = ms.ToArray();
            }
        }
        
        return imageBytes;
    }

    public async Task<string?> UploadImageAsync(byte[] imageBytes, Guid ownerId, CancellationToken ct)
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

            try
            {
                var response = await s3.PutObjectAsync(putRequest, ct);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Wasabi failed on put operation.");

                return null;
            }
        }
             
        return assignedFilename;
    }
}