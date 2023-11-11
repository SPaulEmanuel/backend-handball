using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using aplicatieHandbal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aplicatieHandbal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService!; 
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPlayers()
        {

            var players = await _playerService.GetAllPlayers();
            return Ok(players);
        }
        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] Player playerRequest)
        {
            
            var player=await _playerService.AddPlayer(playerRequest);
            return Ok(player);
        }
        [HttpGet]

        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid id)
        {
            var player = await _playerService.GetPlayerById(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }
       
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updatePlayer([FromRoute] Guid id, Player updatePlayerReq)
        {
            var player = await _playerService.UpdatePlayer(id, updatePlayerReq);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deletePlayer([FromRoute] Guid id)
        {
            var player = await _playerService.DeletePlayer(id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }
    }
}
