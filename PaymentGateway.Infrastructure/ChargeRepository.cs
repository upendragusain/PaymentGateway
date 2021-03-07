using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure
{
    public class ChargeRepository : IChargeRepository
    {
        private readonly PaymentGatewayContext _context;
        private readonly IEncryptionService _encryptionService;

        public ChargeRepository(PaymentGatewayContext context,
                                IEncryptionService encryptionService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _encryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
        }

        public async Task<int> Create(Charge charge)
        {
            charge.Card.Encrypt(_encryptionService);
            await _context.Charges.AddAsync(charge);
            return await _context.SaveChangesAsync();
        }

        public async Task<Charge> Get(Guid merchantId, Guid id)
        {
            var charge = await _context.Charges
                .Include(x => x.Card)
                .SingleAsync(x => 
                x.MerchantId == merchantId 
                && x.Id == id);

            charge.Card.Decrypt(_encryptionService);

            return charge;
        }

        public Task<IEnumerable<Charge>> GetList(Guid merchantId, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
