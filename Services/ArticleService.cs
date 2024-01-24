using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace aplicatieHandbal.Services
{
    public interface IArticleService
    {
        Task<List<Articole>> GetAllArticole();
        Task<Articole> CreateArticle(Articole articol);
        Task<Articole> GetArticoleById(Guid id);
        Task<Articole> UpdateArticole(Guid id, Articole updateArticoleReq);
        Task<Articole> DeleteArticole(Guid id);
        Task<Articole> updateArticlePatch(Guid id, JsonPatchDocument updatedArticleReq);
    }
    public class ArticleService: IArticleService
    {

        private readonly AplicatieDBContext _aplicatieDBContext;
        

        public ArticleService(AplicatieDBContext aplicatieDBContext)
        {
            _aplicatieDBContext = aplicatieDBContext;
            
        }
        public async Task<Articole> CreateArticle(Articole articol)
        {
            _aplicatieDBContext.Articole.Add(articol);
            await _aplicatieDBContext.SaveChangesAsync();

            return articol;
        }
        public async Task<Articole> DeleteArticole(Guid id)
        {
            var articol = await _aplicatieDBContext.Articole.FindAsync(id);
            if (articol is not null)
            {
                _aplicatieDBContext.Articole.Remove(articol);
                await _aplicatieDBContext.SaveChangesAsync();
                return articol;
            }
            throw new Exception("Cannot delete id");
        }

        public async Task<List<Articole>> GetAllArticole()
        {
            var articole= await _aplicatieDBContext.Articole.ToListAsync();
            return articole;
        }

        public async Task<Articole> GetArticoleById(Guid id)
        {
            var articol = await _aplicatieDBContext.Articole.FirstOrDefaultAsync(x => x.ArticoleID == id);
            if (articol is not null)
            {
                return articol;

            }
            throw new Exception(" articol not found");
        }

        public async Task<Articole> updateArticlePatch(Guid id, JsonPatchDocument updatedArticleReq)
        {
            var articol = await _aplicatieDBContext.Articole.FindAsync(id);
            if (articol != null)
            {
                updatedArticleReq.ApplyTo(articol);
                await _aplicatieDBContext.SaveChangesAsync();
                return articol;
            }
            throw new Exception("articol not found ");
        }

        public async Task<Articole> UpdateArticole(Guid id, Articole updateArticoleReq)
        {
            var articol = await _aplicatieDBContext.Articole.FindAsync(id);
            if (articol is not null) {
                articol.Title = updateArticoleReq.Title;
                articol.Author = updateArticoleReq.Author;
                articol.Content = updateArticoleReq.Content;
                articol.DatePublished = updateArticoleReq.DatePublished;
                articol.ImageUrl = updateArticoleReq.ImageUrl;
                await _aplicatieDBContext.SaveChangesAsync();
                return articol;
            }
            throw new Exception("ID not found");
        }
    }
}
