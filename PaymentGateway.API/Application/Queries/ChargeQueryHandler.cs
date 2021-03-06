using MediatR;
using PaymentGateway.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.API.Application.Queries
{
    public class ChargeQueryHandler : IRequestHandler<ChargeQuery, Charge>
    {
        public IChargeRepository _chargeRepository { get; }

        public ChargeQueryHandler(IChargeRepository chargeRepository)
        {
            _chargeRepository = chargeRepository ?? throw new ArgumentNullException(nameof(chargeRepository));
        }

        public async Task<Charge> Handle(ChargeQuery request, CancellationToken cancellationToken)
        {
            return await _chargeRepository.Get(request.MerchantId, request.Id);
        }
    }
}
