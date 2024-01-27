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
        private readonly IPlayerService _playerService;
        public StorageController(IAzureStorage storage, IPlayerService playerService)
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


        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(Guid playerId, IFormFile file)
        {
            // First, check if the player with the given ID exists
            var player = await _playerService.GetPlayerById(playerId);
            if (player == null)
            {
                return NotFound($"Player with ID {playerId} not found.");
            }

            // Now, proceed with the file upload
            BlobResponseDto? response = await _storage.UploadAsync(playerId, file);

            // Check if we got an error
            if (response.Error)
            {
                // We got an error during upload, return an error with details to the client
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                // Update the player with the image URL
                player.ImageUrl = response.Blob.Uri; // Accessing the URI property from BlobDto
                await _playerService.UpdatePlayer(playerId, player); // Update the player with the new image URL

                // Return a success message to the client about successful upload
                return StatusCode(StatusCodes.Status200OK, response);
            }
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
