using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace aplicatieHandbal.Models
{
    public class Game
    {
        public Guid GameID { get; set; }
        [ForeignKey("PlayerID")]
        public Guid PlayerID { get; set; }
        public string Title { get; set; }
 
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Result { get; set; }
        public string  Status { get; set; }
        public string Description { get; set; }
        public int Attendance { get; set; }
        public string MediaUrl { get; set; }

    }



}
