using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Storage.Blob;
using aplicatieHandbal.Data;
using Microsoft.EntityFrameworkCore;
using aplicatieHandbal.Models;
namespace aplicatieHandbal.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly AplicatieDBContext _aplicatieDBContext;
        public ImageController(AplicatieDBContext aplicatieDBContext) { 
            _aplicatieDBContext = aplicatieDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetHomepageImage()
        {
            return Ok( await _aplicatieDBContext.Imagini.ToListAsync());
           
        }
    }
}
