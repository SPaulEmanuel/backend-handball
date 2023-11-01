﻿using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace aplicatieHandbal.Models
{
    public class Game
    {
        public Guid GameID { get; set; }
        public Guid PlayerId { get; set; }
        
        public Player Player { get; set; }
 
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int Result {  get; set; }
        public string Referee { get; set; }
        public string Status { get; set; } 
        public string Description { get; set; }
        public int Attendance { get; set; }
        public string MediaUrl { get; set; }
        
    }
}