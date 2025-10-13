using System.Text.RegularExpressions;
using EVChargingStation.Application.Interfaces.Commons;
using EVChargingStation.Application.Utils;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace EVChargingStation.Application.Services.Commons;

/// <summary>
///     NOTE: This service can be reused in other projects.
///     This service is used to interact with a MinIO server.
///     It requires the following environment variables to be set:
///     - MINIO_ENDPOINT (e.g., "localhost:9000" or "minio.example.com"). Port 9000 is the MinIO API, Port 9001 is the
///     MinIO UI.
///     - MINIO_ACCESS_KEY
///     - MINIO_SECRET_KEY
///     - MINIO_USE_SSL (optional, defaults to auto-detection based on endpoint)
/// </summary>
public class BlobService : IBlobService
{
    private readonly string _bucketName = "uniseapshop-bucket";
    private readonly ILoggerService _logger;
    private readonly IMinioClient _minioClient;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BlobService" /> class.
    ///     Configures the MinIO client using environment variables.
    /// </summary>
    /// <param name="logger">The logger service for logging information and errors.</param>
    /// <exception cref="Exception">Throws if the MinIO client fails to initialize.</exception>
    public BlobService(ILoggerService logger)
    {
        _logger = logger;

        var endpointRaw = Environment.GetEnvironmentVariable("MINIO_ENDPOINT");
        var accessKey = Environment.GetEnvironmentVariable("MINIO_ACCESS_KEY");
        var secretKey = Environment.GetEnvironmentVariable("MINIO_SECRET_KEY");
        var useSslEnv = Environment.GetEnvironmentVariable("MINIO_USE_SSL");

        _logger.Info("Initializing BlobService...");
        _logger.Info($"Raw MINIO_ENDPOINT: {endpointRaw}");

        // Normalize endpoint and auto-detect SSL
        // Default to localhost:9000, which is the default MinIO API port.
        // The MinIO UI is typically on port 9001.
        var endpoint = endpointRaw?.Trim() ?? "localhost:9000";
        var useSsl = false;

        if (bool.TryParse(useSslEnv, out var parsedSsl))
        {
            useSsl = parsedSsl;
        }
        else
        {
            // Auto-detect SSL: use SSL for domain names, not for IP addresses or localhost
            var isIpAddress = Regex.IsMatch(
                endpoint.Split(':')[0],
                @"^\d{1,3}(\.\d{1,3}){3}$"
            );
            var isLocalhost = endpoint.StartsWith("localhost", StringComparison.OrdinalIgnoreCase);

            useSsl = !isIpAddress && !isLocalhost;
        }

        if (endpoint.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
        {
            endpoint = endpoint.Substring(7);
            useSsl = false;
        }
        else if (endpoint.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            endpoint = endpoint.Substring(8);
            useSsl = true;
        }

        _logger.Info($"Normalized endpoint: {endpoint}");
        _logger.Info($"Using SSL: {useSsl}");

        try
        {
            _minioClient = new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .WithSSL(useSsl)
                .Build();

            _logger.Success("MinIO client initialized successfully.");
        }
        catch (Exception ex)
        {
            _logger.Error($"Failed to initialize MinIO client: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    ///     Uploads a file to the MinIO bucket.
    ///     If the bucket does not exist, it will be created automatically.
    /// </summary>
    /// <param name="fileName">The name of the file (object) to be created in the bucket.</param>
    /// <param name="fileStream">The stream containing the file data.</param>
    /// <example>
    ///     <code>
    /// await _blobService.UploadFileAsync("my-image.jpg", fileStream);
    /// </code>
    /// </example>
    public async Task UploadFileAsync(string fileName, Stream fileStream)
    {
        _logger.Info($"Starting file upload: {fileName}");

        try
        {
            var beArgs = new BucketExistsArgs().WithBucket(_bucketName);
            var found = await _minioClient.BucketExistsAsync(beArgs);
            if (!found)
            {
                _logger.Warn($"Bucket '{_bucketName}' not found. Creating a new one...");
                var mbArgs = new MakeBucketArgs().WithBucket(_bucketName);
                await _minioClient.MakeBucketAsync(mbArgs);
                _logger.Success($"Bucket '{_bucketName}' created successfully.");
            }

            var contentType = GetContentType(fileName);

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType);

            await _minioClient.PutObjectAsync(putObjectArgs);
            _logger.Success($"File '{fileName}' uploaded successfully.");
        }
        catch (MinioException minioEx)
        {
            _logger.Error($"MinIO Error during upload: {minioEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.Error($"Unexpected error during file upload: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    ///     Generates a temporary, presigned URL to preview a file.
    ///     This URL is valid for a limited time (e.g., 7 days).
    /// </summary>
    /// <param name="fileName">The name of the file in the bucket.</param>
    /// <returns>A string containing the presigned URL.</returns>
    public async Task<string> GetPreviewUrlAsync(string fileName)
    {
        try
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithExpiry(7 * 24 * 60 * 60); // 7 days

            var previewUrl = await _minioClient.PresignedGetObjectAsync(args);
            _logger.Success($"Preview URL generated: {previewUrl}");
            return previewUrl;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error generating preview URL: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    ///     Generates a temporary, presigned URL to access or download a file.
    ///     This URL is valid for a limited time (e.g., 7 days).
    /// </summary>
    /// <param name="fileName">The name of the file in the bucket.</param>
    /// <returns>A string containing the presigned URL.</returns>
    public async Task<string> GetFileUrlAsync(string fileName)
    {
        try
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithExpiry(7 * 24 * 60 * 60); // 7 days

            var fileUrl = await _minioClient.PresignedGetObjectAsync(args);
            _logger.Success($"Presigned file URL generated: {fileUrl}");
            return fileUrl;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error generating file URL: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    ///     Deletes a file from the MinIO bucket.
    /// </summary>
    /// <param name="fileName">The name of the file to delete.</param>
    public async Task DeleteFileAsync(string fileName)
    {
        _logger.Info($"Deleting file: {fileName}");

        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName);

            await _minioClient.RemoveObjectAsync(removeObjectArgs);
            _logger.Success($"File '{fileName}' deleted successfully.");
        }
        catch (MinioException minioEx)
        {
            _logger.Error($"MinIO Error during delete: {minioEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.Error($"Unexpected error during file delete: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    ///     Replaces an existing image with a new one. It first deletes the old image (if a URL is provided)
    ///     and then uploads the new image, returning its preview URL.
    /// </summary>
    /// <param name="newImageStream">The stream of the new image to upload.</param>
    /// <param name="originalFileName">The original file name of the new image, used to determine the file extension.</param>
    /// <param name="oldImageUrl">The URL of the old image to delete. Can be null or empty if there is no old image.</param>
    /// <param name="containerPrefix">
    ///     A prefix for the file name, typically used to organize files (e.g., "avatars",
    ///     "products").
    /// </param>
    /// <returns>The presigned preview URL of the newly uploaded image.</returns>
    public async Task<string> ReplaceImageAsync(Stream newImageStream, string originalFileName, string? oldImageUrl,
        string containerPrefix)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(oldImageUrl))
                try
                {
                    var oldFileName = Path.GetFileName(new Uri(oldImageUrl).LocalPath);
                    var fullOldPath = $"{containerPrefix}/{oldFileName}";
                    await DeleteFileAsync(fullOldPath);
                    _logger.Info($"[ReplaceImageAsync] Deleted old image: {fullOldPath}");
                }
                catch (Exception ex)
                {
                    _logger.Warn($"[ReplaceImageAsync] Failed to delete old image: {ex.Message}");
                }

            var newFileName = $"{containerPrefix}/{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            _logger.Info($"[ReplaceImageAsync] Uploading new image: {newFileName}");

            await UploadFileAsync(newFileName, newImageStream);

            var previewUrl = await GetPreviewUrlAsync(newFileName);
            _logger.Success($"[ReplaceImageAsync] Uploaded and generated preview URL: {previewUrl}");
            return previewUrl;
        }
        catch (Exception ex)
        {
            _logger.Error($"[ReplaceImageAsync] Error occurred: {ex.Message}");
            throw ErrorHelper.Internal("Lỗi khi xử lý ảnh.");
        }
    }

    /// <summary>
    ///     Determines the MIME content type based on the file extension.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <returns>A string representing the MIME type.</returns>
    private string GetContentType(string fileName)
    {
        _logger.Info($"Determining content type for file: {fileName}");
        var extension = Path.GetExtension(fileName)?.ToLower();

        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".pdf" => "application/pdf",
            ".mp4" => "video/mp4",
            _ => "application/octet-stream" // Fallback for unknown types
        };
    }
}