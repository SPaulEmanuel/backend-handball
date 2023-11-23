namespace aplicatieHandbal.Models
{
    public class Staff
    {
        public Guid StaffID { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public byte[] ImageUrl { get; set; }
    }
}
