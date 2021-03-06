using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure
{
    public class ChargeRepository : IChargeRepository
    {
        private readonly PaymentGatewayContext _context;

        public ChargeRepository(PaymentGatewayContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> Create(Charge charge)
        {
            await _context.Charges.AddAsync(charge);
            return await _context.SaveChangesAsync();
        }

        public async Task<Charge> Get(Guid merchantId, Guid id)
        {
            return await _context.Charges.FindAsync(merchantId, id);
        }

        public Task<IEnumerable<Charge>> GetList(Guid merchantId, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
