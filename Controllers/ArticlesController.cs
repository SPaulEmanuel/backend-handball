using aplicatieHandbal.Data;
using aplicatieHandbal.Helpers;
using aplicatieHandbal.Models;
using aplicatieHandbal.Services;

using CSU_Suceava_BE.Application.JwtUtils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;



namespace aplicatieHandbal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticoleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticoleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllArticole()
        { 
            return Ok(await _articleService.GetAllArticole());
        }

        [AuthorizeMultiplePolicy(Policies.Administrator + ";" + Policies.CreatorDeContinut, false)]
        [HttpPost]
        [Route("/articles")]
        public async Task<IActionResult> CreateArticle([FromBody]  Articole articol)
        {
           return Ok(_articleService.CreateArticle(articol));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetArticoleById([FromRoute] Guid id)
        {

            return Ok(await _articleService.GetArticoleById(id));
        }

        [AuthorizeMultiplePolicy(Policies.Administrator + ";" + Policies.CreatorDeContinut, false)]
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateArticole([FromRoute] Guid id, Articole updateArticoleReq)
        {
            return Ok(await _articleService.UpdateArticole(id, updateArticoleReq));
        }

        [AuthorizeMultiplePolicy(Policies.Administrator + ";" + Policies.CreatorDeContinut, false)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteArticole([FromRoute] Guid id)
        {
      
            return Ok(await _articleService.DeleteArticole(id));
        }

        [AuthorizeMultiplePolicy(Policies.Administrator + ";" + Policies.CreatorDeContinut, false)]
        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateArticlePatch([FromRoute] Guid id, JsonPatchDocument updatedArticleReq)
        {
            return Ok(await _articleService.updateArticlePatch(id, updatedArticleReq));
        }
    }
}
