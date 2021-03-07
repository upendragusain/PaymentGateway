using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure
{
    public class Seeder
    {
        public static void Seed(PaymentGatewayContext context)
        {
            if (!context.Merchants.Any())
            {
                 context.Merchants.Add(new Domain.Merchant(
                     new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"), 
                     "Amazon", 
                     "SW1 2NH"));
                 context.SaveChanges();
            }
        }
    }
}
