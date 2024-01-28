using aplicatieHandbal.Data;
using aplicatieHandbal.Helpers;
using aplicatieHandbal.Models;
using aplicatieHandbal.Services;

using CSU_Suceava_BE.Application.JwtUtils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;


namespace aplicatieHandbal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;


        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService!; 
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetPlayersByPosition")]
        public async Task<IActionResult> GetPlayersByPosition()
        {
            var playersByPosition = await _playerService.GetPlayersByPosition();
            return Ok(playersByPosition);
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPlayers()
        {
  
            return Ok(await _playerService.GetAllPlayers());
        }

        [AllowAnonymous]
        [HttpGet("GetAllInfo")]
        public async Task<IActionResult> GetAllInfoPlayers()
        {
            return Ok(await _playerService.GetAllInfoPlayers());
        }

        [AuthorizeMultiplePolicy(Policies.Administrator, true)]
        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromForm] Player model)
        {
            return Ok(await _playerService.AddPlayer(model));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid id)
        {   
            return Ok(await _playerService.GetPlayerById(id));
        }

        [AllowAnonymous]
        [HttpPatch]
        [Route("{id:Guid}")]

        public async Task<IActionResult> updatePlayerPatch([FromRoute] Guid id, JsonPatchDocument updatePlayerReq)
        {
            return Ok(await _playerService.updatePlayerPatch(id,updatePlayerReq));
        }

        [AuthorizeMultiplePolicy(Policies.Administrator, true)]
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updatePlayer([FromRoute] Guid id, Player updatePlayerReq)
        {
            return Ok(await _playerService.UpdatePlayer(id, updatePlayerReq));
        }

        [AuthorizeMultiplePolicy(Policies.Administrator, true)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deletePlayer([FromRoute] Guid id)
        {
            return Ok(await _playerService.DeletePlayer(id));
        }
    }
}
