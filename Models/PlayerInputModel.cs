using System.ComponentModel.DataAnnotations;

namespace aplicatieHandbal.Models
{
    public class PlayerInputModel
    {
        public string Name { get; set; }
        public string Surname { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Position { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Nationality { get; set; }
        public int JerseyNumber { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public decimal Salary { get; set; }
        public int GoalsScored { get; set; }
        public string InstagramProfile { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
