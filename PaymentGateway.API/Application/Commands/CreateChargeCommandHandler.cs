using MediatR;
using PaymentGateway.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.API.Application.Commands
{
    public class CreateChargeCommandHandler
                : IRequestHandler<CreateChargeCommand, bool>
    {
        public async Task<bool> Handle(CreateChargeCommand request, CancellationToken cancellationToken)
        {
            //throw new Exception("hhhhhhhh");

            var card = new Card(request.brand,
                                request.expiryMonth,
                                request.expiryYear,
                                request.lastFourDigits,
                                request.cvv,
                                request.is3DSecure);

            var address = new Address(request.city,
                                      request.country,
                                      request.line1,
                                      request.line2,
                                      request.postalCode,
                                      request.state);

            var billingDetail = new BillingDetail(request.email,
                                      request.name,
                                      request.phone,
                                      address);

            var charge = new Charge(request.amount,
                                    request.currencyCode,
                                    billingDetail,
                                    card,
                                    request.idempotencyKey);

            return await Task.FromResult(true);
        }
    }
}
