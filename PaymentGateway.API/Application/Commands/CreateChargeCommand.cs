using MediatR;
using PaymentGateway.API.Integration.Bank;
using PaymentGateway.Domain;
using System;

namespace PaymentGateway.API.Application.Commands
{
    public class CreateChargeCommand : IRequest<PaymentResponse>
    {
        public Guid MerchantId { get; set; }
        public decimal Amount { get; set; }
        public Currency CurrencyCode { get; set; }

        public Brand Brand { get; set; }
        public byte ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public bool Is3DSecure { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }


        public string City { get; set; }
        public string Country { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }

        public Guid IdempotencyKey { get; set; }
    }
}
