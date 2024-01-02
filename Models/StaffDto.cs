namespace aplicatieHandbal.Models
{
    public class StaffDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public object Position { get; internal set; }
    }
}
