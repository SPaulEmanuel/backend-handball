using System.ComponentModel.DataAnnotations.Schema;

namespace aplicatieHandbal.Models
{
    public class Player
    {
        public Guid PlayerID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public string Position { get; set; }
        public int JerseyNumber { get; set; }   
        public int GoalsScored { get; set; }
        public string ImageUrl { get; set; }
       // https://ipstorage1989.blob.core.windows.net/ipcontainer/echipaHome.png
       

    }
}
