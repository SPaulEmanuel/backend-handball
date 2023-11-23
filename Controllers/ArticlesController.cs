using aplicatieHandbal.Data;
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
    public class ArticoleController : Controller
    {
        private readonly IArticleService _playerService;

        public ArticoleController(IArticleService playerService)
        {
           _playerService = playerService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllArticole()
        { 
            return Ok(await _playerService.GetAllArticole());
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromForm] ArticleInputModel articol)
        { 
            return Ok(await _playerService.CreateArticle(articol));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetArticoleById([FromRoute] Guid id)
        {

            return Ok(await _playerService.GetArticoleById(id));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateArticole([FromRoute] Guid id, Articole updateArticoleReq)
        {
            return Ok(await _playerService.UpdateArticole(id, updateArticoleReq));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteArticole([FromRoute] Guid id)
        {
      
            return Ok(await _playerService.DeleteArticole(id));
        }
        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateArticlePatch([FromRoute] Guid id, JsonPatchDocument updatedArticleReq)
        {
            return Ok(await _playerService.updateArticlePatch(id, updatedArticleReq));
        }
    }
}
