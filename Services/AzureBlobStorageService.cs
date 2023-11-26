using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using Microsoft.Extensions.Configuration;

namespace aplicatieHandbal.Services
{
    public class AzureBlobStorageService
    {
        private readonly CloudBlobClient _blobClient;

        public AzureBlobStorageService(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("StorageConnection");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<string> UploadImageAsync(string containerName, IFormFile file)
        {
            var container = _blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();

            // Generate a unique filename
            string uniqueFilename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var blockBlob = container.GetBlockBlobReference(uniqueFilename);
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return blockBlob.Uri.ToString();
        }
    }

}
