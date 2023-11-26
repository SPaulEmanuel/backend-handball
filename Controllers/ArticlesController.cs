using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using aplicatieHandbal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
namespace aplicatieHandbal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticoleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticoleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllArticole()
        { 
            return Ok(await _articleService.GetAllArticole());
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(string title, string author, string content, DateTime datePublished, [FromForm] IFormFile image)
        {
            var createdArticle = await _articleService.CreateArticle (title, author, content,datePublished,image);

            return Ok(createdArticle);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetArticoleById([FromRoute] Guid id)
        {

            return Ok(await _articleService.GetArticoleById(id));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateArticole([FromRoute] Guid id, Articole updateArticoleReq)
        {
            return Ok(await _articleService.UpdateArticole(id, updateArticoleReq));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteArticole([FromRoute] Guid id)
        {
      
            return Ok(await _articleService.DeleteArticole(id));
        }
        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateArticlePatch([FromRoute] Guid id, JsonPatchDocument updatedArticleReq)
        {
            return Ok(await _articleService.updateArticlePatch(id, updatedArticleReq));
        }
    }
}
