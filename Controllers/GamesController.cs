using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using aplicatieHandbal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aplicatieHandbal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService!;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            return Ok(await _gameService.GetAllGames());
        }
        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] Game gameRequest)
        {
            return Ok(await _gameService.AddGame(gameRequest));
        }
        [HttpGet]

        [Route("{id:Guid}")]
        public async Task<IActionResult> GetGame([FromRoute] Guid id)
        {
            return Ok(await _gameService.GetGameById(id));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateGame([FromRoute] Guid id, Game updateGameReq)
        {
            return Ok(await _gameService.UpdateGame(id, updateGameReq));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteGame([FromRoute] Guid id)
        {
            return Ok(await _gameService.DeleteGame(id));
        }

    }
}
