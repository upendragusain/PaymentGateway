using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain;

namespace PaymentGateway.Infrastructure
{
    public class PaymentGatewayContext : DbContext
    {
        public PaymentGatewayContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Charge> Charges { get; set; }

        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(
            //    @"Server=(localdb)\mssqllocaldb;Database=Reservations;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Charge>()
               .HasKey(p => new { p.MerchantId, p.Id });
        }
    }
}
