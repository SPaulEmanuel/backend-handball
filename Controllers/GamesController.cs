using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aplicatieHandbal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        private readonly AplicatieDBContext _aplicatieDBContext;
        public GamesController(AplicatieDBContext aplicatieDBContext)
        {
            _aplicatieDBContext = aplicatieDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var games = await _aplicatieDBContext.Games.ToListAsync();
            return Ok(games);
        }
        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] Game gameRequest)
        {
            gameRequest.Id = Guid.NewGuid();
            await _aplicatieDBContext.Games.AddAsync(gameRequest);
            await _aplicatieDBContext.SaveChangesAsync();
            return Ok(gameRequest);
        }
        [HttpGet]

        [Route("{id:Guid}")]
        public async Task<IActionResult> GetGame([FromRoute] Guid id)
        {
            var game = await _aplicatieDBContext.Games.FirstOrDefaultAsync(x => x.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateGame([FromRoute] Guid id, Game updateGameReq)
        {
            var game = await _aplicatieDBContext.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            game.Title = updateGameReq.Title;
            game.Date = updateGameReq.Date;
            game.Location = updateGameReq.Location;
            game.Result = updateGameReq.Result;
            game.Referee = updateGameReq.Referee;
            game.Status = updateGameReq.Status;
            game.Description = updateGameReq.Description;
            game.Attendance = updateGameReq.Attendance;
            game.MediaUrl = updateGameReq.MediaUrl;

            await _aplicatieDBContext.SaveChangesAsync();
            return Ok(game);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteGame([FromRoute] Guid id)
        {
            var game = await _aplicatieDBContext.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            _aplicatieDBContext.Games.Remove(game);
            await _aplicatieDBContext.SaveChangesAsync();
            return Ok(game);
        }

    }
}
