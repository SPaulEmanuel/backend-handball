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
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

      
        private readonly AplicatieDBContext _aplicatieDBContext;
        public PlayerController( AplicatieDBContext aplicatieDBContext)
        {
            _aplicatieDBContext = aplicatieDBContext!;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var players = await _aplicatieDBContext.Players.ToListAsync();
            return Ok(players);
        }
        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] Player playerRequest)
        {
            playerRequest.PlayerID = Guid.NewGuid();
            await _aplicatieDBContext.Players.AddAsync(playerRequest);
            await _aplicatieDBContext.SaveChangesAsync();
            return Ok(playerRequest);
        }
        [HttpGet]

        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var player = await _aplicatieDBContext.Players.FirstOrDefaultAsync(x => x.PlayerID == id);
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
            var player = await _aplicatieDBContext.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            player.Name = updatePlayerReq.Name;
            player.Vorname = updatePlayerReq.Vorname;
            player.Age = updatePlayerReq.Age;
            player.Position = updatePlayerReq.Position;
            player.Height = updatePlayerReq.Height;
            player.Weight = updatePlayerReq.Weight;
            player.Nationality = updatePlayerReq.Nationality;
            player.JerseyNumber = updatePlayerReq.JerseyNumber;
            player.ContractStartDate = updatePlayerReq.ContractStartDate;
            player.ContractEndDate = updatePlayerReq.ContractEndDate;
            player.Salary = updatePlayerReq.Salary;
            player.GoalsScored = updatePlayerReq.GoalsScored;
            player.ImageUrl = updatePlayerReq.ImageUrl;
            player.InstagramProfile = updatePlayerReq.InstagramProfile;

            await _aplicatieDBContext.SaveChangesAsync();
            return Ok(player);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deletePlayer([FromRoute] Guid id)
        {
            var player = await _aplicatieDBContext.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            _aplicatieDBContext.Players.Remove(player);
            await _aplicatieDBContext.SaveChangesAsync();
            return Ok(player);
        }
    }
}
