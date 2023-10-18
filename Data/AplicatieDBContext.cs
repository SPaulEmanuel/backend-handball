using aplicatieHandbal.Models;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Articole> Articles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Player)        // Each game has one player
                .WithMany()                   // Each player can be associated with multiple games
                .HasForeignKey(g => g.PlayerId);

           
            modelBuilder.Entity<Player>()
                .Property(p => p.Salary)
                .HasColumnType("decimal(18, 2)");


            base.OnModelCreating(modelBuilder);
        }
  


    }
}
