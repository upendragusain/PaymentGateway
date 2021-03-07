using System;

namespace PaymentGateway.API.Application.Queries
{
    public class PaymentDetail
    {
        public Guid Id { get; set; }
        public Guid MerchantId { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string LastFourDigits { get; set; }
        public string Status { get; set; }
    }
}