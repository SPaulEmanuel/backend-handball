using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using aplicatieHandbal.Validators;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace aplicatieHandbal.Services
{
    public interface IPlayerService
    {
        Task<List<PlayerDto>> GetAllPlayers();
        Task<List<Player>> GetAllInfoPlayers();
        Task<List<List<PlayerDto>>> GetPlayersByPosition();
        Task<Player> AddPlayer(Player model);
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
        public async Task<Player> AddPlayer(Player model)
        {
            var validator = new PlayerValidator();
            validator.ValidateAndThrow(model);
            model.PlayerID = Guid.NewGuid();
            _aplicatieDBContext.Players.Add(model);
            await _aplicatieDBContext.SaveChangesAsync();

            return model;

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
                    ImageUrl=player.ImageUrl
                   
                }) 
                .ToListAsync();

            return players;
        }
        // PlayerService.cs


        public async Task<List<List<PlayerDto>>> GetPlayersByPosition()
        {
            var playersByPosition = await _aplicatieDBContext.Players
                .GroupBy(player => player.Position)
                .Select(group => group.Select(player => new PlayerDto
                {
                    Name = player.Name,
                    Surname = player.Surname,
                    ImageUrl = player.ImageUrl,
                    Position = player.Position  
                }).ToList())
                .ToListAsync();

            return playersByPosition;
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
                player.GoalsScored = updatePlayerReq.GoalsScored;
                player.JerseyNumber = updatePlayerReq.JerseyNumber;

                await _aplicatieDBContext.SaveChangesAsync();
                return player;
            }
            throw new Exception("ID not found");
        }
    }
}
