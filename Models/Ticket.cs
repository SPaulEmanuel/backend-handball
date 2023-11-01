using System.ComponentModel.DataAnnotations.Schema;

namespace aplicatieHandbal.Models
{
    public class Ticket
    {
        public Guid TicketID { get; set; }
        
        [ForeignKey("GameId")]
        public Guid GameID { get; set; }
        public Game Game { get; set; }
        public string Name { get; set; } 

        public decimal Price { get; set; } 

        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
    }
}
