using MediatR;
using PaymentGateway.API.Integration.Bank;
using PaymentGateway.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.API.Application.Commands
{
    public class CreateChargeCommandHandler
                : IRequestHandler<CreateChargeCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IAquiringBankApiService _aquiringBankApiService;
        private readonly IChargeRepository _chargeRepository;

        public CreateChargeCommandHandler(IMediator mediator,
                                          IAquiringBankApiService aquiringBankApiService,
                                          IChargeRepository chargeRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _aquiringBankApiService = aquiringBankApiService ?? throw new ArgumentNullException(nameof(aquiringBankApiService));
            _chargeRepository = chargeRepository ?? throw new ArgumentNullException(nameof(chargeRepository));
        }

        public async Task<string> Handle(CreateChargeCommand request, CancellationToken cancellationToken)
        {
            //Todo: check if card already exists, else would create new card with same details each time!
            var card = new Card(request.Brand,
                                request.ExpiryMonth,
                                request.ExpiryYear,
                                request.CardNumber,
                                request.CVV,
                                request.Is3DSecure);

            var charge = new Charge(request.Amount,
                                    request.CurrencyCode,
                                    card,
                                    request.IdempotencyKey);

            // make call to acquiring bank
            var paymentResponse = await _aquiringBankApiService.RequestPayment(
                                        new BankPaymentRequest(charge.Amount,
                                            charge.Currency,
                                            charge.Card.Brand,
                                            charge.Card.ExpiryMonth,
                                            charge.Card.ExpiryYear,
                                            charge.Card.Number,
                                            charge.Card.Cvv));

            charge.AddPaymentResponse(paymentResponse.PaymentResponseId,
                                      paymentResponse.Status,
                                      paymentResponse.FailureCode);

            //persist to db
            await _chargeRepository.Create(charge);
            return paymentResponse.Status;
        }
    }
}
