using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using aplicatieHandbal.Validators;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aplicatieHandbal.Services
{
    public interface IPlayerService
    {
        Task<List<PlayerDto>> GetAllPlayers();
        Task<List<Player>> GetAllInfoPlayers();
        Task<Player> AddPlayer(PlayerInputModel model);
        Task<Player> GetPlayerById(Guid id);
        Task<Player> UpdatePlayer(Guid id, Player updatedPlayer);
        Task<Player> DeletePlayer(Guid id);
        Task<Player> updatePlayerPatch(Guid id, JsonPatchDocument updatedPlayerReq);
    }

    public class PlayerService : IPlayerService
    {
        private readonly AplicatieDBContext _aplicatieDBContext;
        public PlayerService(AplicatieDBContext dbContext)
        {
            _aplicatieDBContext = dbContext;
        }
        public async Task<Player> AddPlayer(PlayerInputModel model)
        {
            var validator = new PlayerValidator();
            validator.ValidateAndThrow(model);

            byte[] imageUrl;
            using (var stream = model.Image.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                imageUrl = memoryStream.ToArray();
            }
            var player = new Player
            {
                Name = model.Name,
                Surname = model.Surname,
                Age = model.Age,
                Position = model.Position,
                Height = model.Height,
                Weight = model.Weight,
                Nationality = model.Nationality,
                JerseyNumber = model.JerseyNumber,  
                ContractStartDate = model.ContractStartDate,
                ContractEndDate = model.ContractEndDate,
                Salary = model.Salary,
                GoalsScored = model.GoalsScored,
                ImageUrl = imageUrl,
                InstagramProfile = model.InstagramProfile
                
            };

            _aplicatieDBContext.Players.Add(player);
            await _aplicatieDBContext.SaveChangesAsync();
            return player;
        }

        public async Task<Player> DeletePlayer(Guid id)
        {
            var player = await _aplicatieDBContext.Players.FindAsync(id);
           if (player is not null) {
                _aplicatieDBContext.Players.Remove(player);
                await _aplicatieDBContext.SaveChangesAsync();
                return player;
            }
           throw new Exception("Cannot delete id");
            
        }

        public async Task<List<Player>> GetAllInfoPlayers()
        {
            var players = await _aplicatieDBContext.Players.ToListAsync();
            return players;
        }

        public async Task<List<PlayerDto>> GetAllPlayers()
        {
            var players = await _aplicatieDBContext.Players
                .Select(player => new PlayerDto
                {
                    Name = player.Name,
                    Surname = player.Surname,
                    ImageUrl = player.ImageUrl
                })
                .ToListAsync();

            return players;
        }


        public async Task<Player> GetPlayerById(Guid id)
        {
            var player = await _aplicatieDBContext.Players.FirstOrDefaultAsync(x => x.PlayerID == id);
            if (player is not null)
            {
                return player;
                
            }
            throw new Exception("Player not found");
        }
        public async Task<Player> updatePlayerPatch(Guid id, JsonPatchDocument updatedPlayerPatch)
        {
            var player = await _aplicatieDBContext.Players.FindAsync(id);
            if (player != null)
            {
                updatedPlayerPatch.ApplyTo(player);
                await _aplicatieDBContext.SaveChangesAsync();
                return player;
            }
            throw new Exception("player not found ");
        }

        public async Task<Player> UpdatePlayer(Guid id, Player updatePlayerReq)
        {

            var player = await _aplicatieDBContext.Players.FindAsync(id);
            if (player is not null)
            {
                //var validator = new PlayerValidator();
                //validator.ValidateAndThrow(updatePlayerReq);
                player.Name = updatePlayerReq.Name;
                player.Surname = updatePlayerReq.Surname;
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
            throw new Exception("ID not found");
        }


    }

}
