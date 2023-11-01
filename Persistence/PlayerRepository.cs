using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using Microsoft.EntityFrameworkCore;

namespace aplicatieHandbal.Persistence
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> AddPlayer(Player player);
        Task<Player> GetPlayerById(Guid id);
        Task<Player> UpdatePlayer(Player updatedPlayer);
        Task<Player> DeletePlayer(Guid id);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly AplicatieDBContext _dbContext;

        public PlayerRepository(AplicatieDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Player> AddPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Task<Player> DeletePlayer(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            // Assuming you are using Entity Framework for data access
            var players = await _dbContext.Players.ToListAsync();
            return players;
        }


        public Task<Player> GetPlayerById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Player> UpdatePlayer(Player updatedPlayer)
        {
            throw new NotImplementedException();
        }

       
    }

}
