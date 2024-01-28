using aplicatieHandbal.Data;
using aplicatieHandbal.Helpers;
using aplicatieHandbal.Models;
using aplicatieHandbal.Services;

using CSU_Suceava_BE.Application.JwtUtils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;


namespace aplicatieHandbal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService!;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            return Ok(await _gameService.GetAllGames());
        }

        [AuthorizeMultiplePolicy(Policies.Administrator + ";" + Policies.CreatorDeContinut, false)]
        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] Game gameRequest)
        {
            return Ok(await _gameService.AddGame(gameRequest));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetGame([FromRoute] Guid id)
        {
            
            return Ok(await _gameService.GetGameById(id));

        }

        [AuthorizeMultiplePolicy(Policies.Administrator + ";" + Policies.CreatorDeContinut, false)]
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateGame([FromRoute] Guid id, Game updateGameReq)
        {
            return Ok(await _gameService.UpdateGame(id, updateGameReq));
        }


        [AuthorizeMultiplePolicy(Policies.Administrator + ";" + Policies.CreatorDeContinut, false)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteGame([FromRoute] Guid id)
        {
            var response = await _gameService.DeleteGame(id);
            if (response == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, response.Status);

            }
            return Ok(await _gameService.DeleteGame(id));
        }

    }
}
