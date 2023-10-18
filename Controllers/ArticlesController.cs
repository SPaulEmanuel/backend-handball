using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace aplicatieHandbal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticoleController : Controller
    {
        private readonly AplicatieDBContext _aplicatieDBContext;

        public ArticoleController(AplicatieDBContext aplicatieDBContext)
        {
            _aplicatieDBContext = aplicatieDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticole()
        {
            var articole = await _aplicatieDBContext.Articole.ToListAsync();
            return Ok(articole);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticole([FromBody] Articole articoleRequest)
        {
            articoleRequest.Id = Guid.NewGuid();
            await _aplicatieDBContext.Articole.AddAsync(articoleRequest);
            await _aplicatieDBContext.SaveChangesAsync();
            return Ok(articoleRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetArticole([FromRoute] Guid id)
        {
            var articole = await _aplicatieDBContext.Articole.FirstOrDefaultAsync(x => x.Id == id);
            if (articole == null)
            {
                return NotFound();
            }
            return Ok(articole);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateArticole([FromRoute] Guid id, Articole updateArticoleReq)
        {
            var articole = await _aplicatieDBContext.Articole.FindAsync(id);
            if (articole == null)
            {
                return NotFound();
            }

            articole.Title = updateArticoleReq.Title;
            articole.Author = updateArticoleReq.Author;
            articole.Content = updateArticoleReq.Content;
            articole.DatePublished = updateArticoleReq.DatePublished;
            articole.ImageUrl = updateArticoleReq.ImageUrl;

            await _aplicatieDBContext.SaveChangesAsync();
            return Ok(articole);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteArticole([FromRoute] Guid id)
        {
            var articole = await _aplicatieDBContext.Articole.FindAsync(id);
            if (articole == null)
            {
                return NotFound();
            }
            _aplicatieDBContext.Articole.Remove(articole);
            await _aplicatieDBContext.SaveChangesAsync();
            return Ok(articole);
        }
    }
}
