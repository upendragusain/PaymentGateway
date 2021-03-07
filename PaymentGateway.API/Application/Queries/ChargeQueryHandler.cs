using MediatR;
using PaymentGateway.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.API.Application.Queries
{
    public class ChargeQueryHandler : IRequestHandler<ChargeQuery, PaymentDetail>
    {
        public IChargeRepository _chargeRepository { get; }

        public ChargeQueryHandler(IChargeRepository chargeRepository)
        {
            _chargeRepository = chargeRepository ?? throw new ArgumentNullException(nameof(chargeRepository));
        }

        public async Task<PaymentDetail> Handle(ChargeQuery request, CancellationToken cancellationToken)
        {
            var charge = await _chargeRepository.Get(request.MerchantId, request.Id);
            return new PaymentDetail()
            {
                Id = charge.Id,
                MerchantId = charge.MerchantId,
                ExpiryMonth = charge.Card.ExpiryMonth,
                ExpiryYear = charge.Card.ExpiryYear,
                LastFourDigits = charge.Card.LastFourDigits,
                Status = charge.Status
            };
        }
    }
}
