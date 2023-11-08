using aplicatieHandbal.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Numerics;

namespace aplicatieHandbal.Data
{
    public class AplicatieDBContext:DbContext
    {
        public AplicatieDBContext(DbContextOptions<AplicatieDBContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Articole> Articole { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Player)        // Each game has one player
                .WithMany()                   // Each player can be associated with multiple games
                .HasForeignKey(g => g.PlayerId);

           
            modelBuilder.Entity<Player>()
                .Property(p => p.Salary)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18, 2)");
     
            modelBuilder.Entity<Staff>().HasData(new { StaffID = Guid.NewGuid(), Name ="dewdew",Vorname="dewd",ImageUrl="empty" }); 
            base.OnModelCreating(modelBuilder);
        }
  


    }
}
