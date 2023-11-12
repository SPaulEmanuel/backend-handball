﻿using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using aplicatieHandbal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aplicatieHandbal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequireHttps]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService!; 
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPlayers()
        {
  
            return Ok(await _playerService.GetAllPlayers());
        }
        [HttpGet("GetAllInfo")]
        public async Task<IActionResult> GetAllInfoPlayers()
        {
            return Ok(await _playerService.GetAllInfoPlayers());
        }
        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] Player playerRequest)
        {
            
            return Ok(await _playerService.AddPlayer(playerRequest));
        }
        [HttpGet]

        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid id)
        {   
            return Ok(await _playerService.GetPlayerById(id));
        }
       
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updatePlayer([FromRoute] Guid id, Player updatePlayerReq)
        {
            return Ok(await _playerService.UpdatePlayer(id, updatePlayerReq));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deletePlayer([FromRoute] Guid id)
        {
            return Ok(await _playerService.DeletePlayer(id));
        }
    }
}
