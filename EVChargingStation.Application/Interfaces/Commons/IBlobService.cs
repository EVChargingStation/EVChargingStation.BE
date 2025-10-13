namespace EVChargingStation.Application.Interfaces.Commons;

public interface IBlobService
{
    /// <summary>
    ///     Tải file lên MinIO với tên và luồng dữ liệu cụ thể.
    /// </summary>
    Task UploadFileAsync(string fileName, Stream fileStream);

    /// <summary>
    ///     Lấy đường dẫn xem trước (preview URL) cho file đã upload.
    /// </summary>
    Task<string> GetPreviewUrlAsync(string fileName);

    /// <summary>
    ///     Lấy URL tải file có thời hạn từ MinIO (Presigned URL).
    /// </summary>
    Task<string> GetFileUrlAsync(string fileName);

    /// <summary>
    ///     Xóa file khỏi MinIO theo tên file.
    /// </summary>
    Task DeleteFileAsync(string fileName);

    /// <summary>
    ///     Thay thế ảnh cũ bằng ảnh mới: xóa ảnh cũ nếu có, upload ảnh mới và trả về preview URL.
    /// </summary>
    Task<string> ReplaceImageAsync(Stream newImageStream, string newImageName, string? oldImageUrl,
        string containerPrefix);
}