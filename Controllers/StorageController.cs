using aplicatieHandbal.Models;
using aplicatieHandbal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aplicatieHandbal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IAzureStorage _storage;
        private readonly PlayerService _playerService;

        public StorageController(IAzureStorage storage,PlayerService playerService)
        {
            _storage = storage;
            _playerService = playerService;
        }

        [HttpGet(nameof(Get))]
        public async Task<IActionResult> Get()
        {
            // Get all files at the Azure Storage Location and return them
            List<BlobDto>? files = await _storage.ListAsync();

            // Returns an empty array if no files are present at the storage container
            return StatusCode(StatusCodes.Status200OK, files);
        }


        /*  [HttpPost(nameof(Upload))]
          public async Task<IActionResult> Upload(IFormFile file)
          {
              BlobResponseDto? response = await _storage.UploadAsync(file);

              // Check if we got an error
              if (response.Error == true)
              {
                  // We got an error during upload, return an error with details to the client
                  return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
              }
              else
              {
                  // Return a success message to the client about successfull upload
                  return StatusCode(StatusCodes.Status200OK, response);
              }
          }*/
        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile file, [FromQuery] Guid playerId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is null or empty");
            }

            BlobResponseDto response = await _storage.UploadAsync(file);

            if (response.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }

            // File uploaded successfully, update Player's ImageUrl
            Player player = await _playerService.GetPlayerById(playerId); 

            if (player == null)
            {
                return NotFound("Player not found");
            }
            player.PlayerID=Guid.NewGuid();
            // Update the player with the uploaded image URL
            await _playerService.UpdatePlayer(playerId, player, response.Blob.Uri);

            return Ok("File uploaded successfully");
        }



        [HttpGet("{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            BlobDto? file = await _storage.DownloadAsync(filename);

            // Check if file was found
            if (file == null)
            {
                // Was not, return error message to client
                return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");
            }
            else
            {
                // File was found, return it to client
                return File(file.Content, file.ContentType, file.Name);
            }
        }

        [HttpDelete("filename")]
        public async Task<IActionResult> Delete(string filename)
        {
            BlobResponseDto response = await _storage.DeleteAsync(filename);

            // Check if we got an error
            if (response.Error == true)
            {
                // Return an error message to the client
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                // File has been successfully deleted
                return StatusCode(StatusCodes.Status200OK, response.Status);
            }
        }
    }
}
