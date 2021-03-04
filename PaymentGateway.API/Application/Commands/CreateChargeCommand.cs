using MediatR;
using PaymentGateway.Domain;
using System;

namespace PaymentGateway.API.Application.Commands
{
    public class CreateChargeCommand : IRequest<bool>
    {
        public decimal amount { get; set; }
        public Currency currencyCode { get; set; }

        public Brand brand { get; set; }
        public byte expiryMonth { get; set; }
        public int expiryYear { get; set; }
        public int lastFourDigits { get; set; }
        public int cvv { get; set; }
        public bool is3DSecure { get; set; }

        public string email { get; set; }
        public string name { get; set; }
        public string phone { get; set; }


        public string city { get; set; }
        public string country { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string postalCode { get; set; }
        public string state { get; set; }

        public Guid idempotencyKey { get; set; }
    }
}
