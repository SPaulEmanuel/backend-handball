namespace aplicatieHandbal.Models
{
    public class Player
    {
        public Guid PlayerID { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
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
        public string ImageUrl { get; set; }
        public string InstagramProfile { get; set; }

    }
}
