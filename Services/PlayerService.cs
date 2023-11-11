using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using Microsoft.EntityFrameworkCore;

namespace aplicatieHandbal.Services
{
    public interface IPlayerService
    {
        Task<List<Player>> GetAllPlayers();
        Task<Player> AddPlayer(Player player);
        Task<Player> GetPlayerById(Guid id);
        Task<Player> UpdatePlayer(Guid id, Player updatedPlayer);
        Task<Player> DeletePlayer(Guid id);
    }

    public class PlayerService : IPlayerService
    {
        private readonly AplicatieDBContext _aplicatieDBContext;
        public PlayerService(AplicatieDBContext dbContext)
        {
            _aplicatieDBContext = dbContext;
        }
        public async Task<Player> AddPlayer(Player playerRequest)
        {
            playerRequest.PlayerID = Guid.NewGuid();
            await _aplicatieDBContext.Players.AddAsync(playerRequest);
            await _aplicatieDBContext.SaveChangesAsync();
            return playerRequest;
        }

        public async Task<Player> DeletePlayer(Guid id)
        {
            var player = await _aplicatieDBContext.Players.FindAsync(id);
           
            _aplicatieDBContext.Players.Remove(player);
            await _aplicatieDBContext.SaveChangesAsync();
            return player;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            var players = await _aplicatieDBContext.Players.ToListAsync();
            return players;
        }

        public async Task<Player> GetPlayerById(Guid id)
        {
            var player = await _aplicatieDBContext.Players.FirstOrDefaultAsync(x => x.PlayerID == id);
            return player;
        }

        public async Task<Player> UpdatePlayer(Guid id, Player updatePlayerReq)
        {
            var player = await _aplicatieDBContext.Players.FindAsync(id);

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
            return player;
        }
    }

}
