using aplicatieHandbal.Models;

namespace aplicatieHandbal.Services
{
    public interface IAzureStorage
    {
       
        Task<BlobResponseDto> UploadAsync(Guid playerId, IFormFile file);
        Task<BlobDto> DownloadAsync(string blobFilename);

        Task<BlobResponseDto> DeleteAsync(string blobFilename);
        Task<List<BlobDto>> ListAsync();
    }
}
