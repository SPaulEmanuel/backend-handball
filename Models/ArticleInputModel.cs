using System.ComponentModel.DataAnnotations;

namespace aplicatieHandbal.Models
{
    public class ArticleInputModel
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public string Content { get; set; }
        public DateTime DatePublished { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
