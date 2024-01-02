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
        public DbSet<Images> Imagini { get; set; }
        public DbSet<User> Login { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<User>().HasKey(login => login.Id);


            modelBuilder.Entity<Images>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
  


    }
}
