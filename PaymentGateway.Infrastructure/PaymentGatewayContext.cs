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

        public DbSet<Merchant> Merchants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ToDo: add some indices here to facilitate querying on most common fields such as merchant id etc
        }
    }
}
