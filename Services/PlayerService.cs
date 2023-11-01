using aplicatieHandbal.Models;
using aplicatieHandbal.Persistence;

namespace aplicatieHandbal.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> AddPlayer(Player player);
        Task<Player> GetPlayerById(Guid id);
        Task<Player> UpdatePlayer(Guid id, Player updatedPlayer);
        Task<Player> DeletePlayer(Guid id);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
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
            // Assuming that _playerRepository.GetAllPlayers() is an asynchronous method
            var players = await _playerRepository.GetAllPlayers();
            return players;
        }

        public Task<Player> GetPlayerById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Player> UpdatePlayer(Guid id, Player updatedPlayer)
        {
            throw new NotImplementedException();
        }

        
    }

}
