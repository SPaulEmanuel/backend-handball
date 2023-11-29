using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Storage.Blob;

namespace aplicatieHandbal.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly string storageAccountName = "ipstorage1989";
        private readonly string containerName = "ipcontainer";
        [HttpGet]
        public async Task<IActionResult> GetHomepageImage()
        {
            string blobName = "echipaHome.png";
            var imageUrl = $"https://{storageAccountName}.blob.core.windows.net/{containerName}/{blobName}";
            return Redirect(imageUrl);
        }
    }
}
