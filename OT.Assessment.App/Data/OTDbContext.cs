using Microsoft.EntityFrameworkCore;
using OT.Assessment.App.Models;

namespace OT.Assessment.App.Data
{
    public class OTDbContext : DbContext
    {
        public OTDbContext(DbContextOptions<OTDbContext> options) : base(options) { }

        public DbSet<CasinoWager> CasinoWagers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CasinoWager>()
                .HasKey(c => c.WagerId); // Define primary key

            // Specify the column type for Amount property
            modelBuilder.Entity<CasinoWager>()
                .Property(c => c.Amount)
                .HasColumnType("decimal(18,2)"); 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("SERVER=localhost; DATABASE=OT_Assessment_DB; Integrated Security=SSPI; Encrypt=False; TrustServerCertificate=True;");
            }
        }
    }
}