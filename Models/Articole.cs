﻿namespace aplicatieHandbal.Models
{
    public class Articole
    {
    
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
        public string ImageUrl { get; set; }


    }
}
