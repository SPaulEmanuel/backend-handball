using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using aplicatieHandbal.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace aplicatieHandbal.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllGames();
        Task<Game> AddGame(Game game);
        Task<Game> GetGameById(Guid id);
        Task<Game> UpdateGame(Guid id, Game updatedGame);
        Task<Game> DeleteGame(Guid id);
    }
    public class GameService : IGameService
    {
        AplicatieDBContext _aplicatieDBContext;
        public GameService(AplicatieDBContext aplicatieDBContext) {
            _aplicatieDBContext= aplicatieDBContext;
        }
        public async Task<Game> AddGame(Game gameRequest)
        {
            var validator = new GameValidator();
            validator.ValidateAndThrow(gameRequest);
            gameRequest.GameID = Guid.NewGuid();
            await _aplicatieDBContext.Games.AddAsync(gameRequest);
            await _aplicatieDBContext.SaveChangesAsync();
            return gameRequest;
        }

        public async Task<Game> DeleteGame(Guid id)
        {
            var game = await _aplicatieDBContext.Games.FindAsync(id);
            if (game is not null)
            {
                _aplicatieDBContext.Games.Remove(game);
                await _aplicatieDBContext.SaveChangesAsync();
                return game;
            }
            throw new Exception("Cannot delete id");
        }

        public async Task<List<Game>> GetAllGames()
        {
            var games = await _aplicatieDBContext.Games.ToListAsync();
            return games;
        }

        public async Task<Game> GetGameById(Guid id)
        {
            var game = await _aplicatieDBContext.Games.FirstOrDefaultAsync(x => x.GameID == id);
            if (game is not null)
            {
                return game;

            }
            throw new Exception("Game not found");
        }

        public async Task<Game> UpdateGame(Guid id, Game updateGameReq)
        {
            var game = await _aplicatieDBContext.Games.FindAsync(id);

            if (game is not null)
            {
                var validator = new GameValidator(); 
                validator.ValidateAndThrow(updateGameReq);

                game.Title = updateGameReq.Title;
                game.Date = updateGameReq.Date;
                game.Location = updateGameReq.Location;
                game.Result = updateGameReq.Result;
                game.Status = updateGameReq.Status;
                game.Description = updateGameReq.Description;
                game.Attendance = updateGameReq.Attendance;
                game.MediaUrl = updateGameReq.MediaUrl;

                await _aplicatieDBContext.SaveChangesAsync();
                return game;
            }

            throw new Exception("ID not found");
        }

    }
}
