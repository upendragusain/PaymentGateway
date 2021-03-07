using PaymentGateway.Domain;

namespace PaymentGateway.API.Integration.Bank
{
    public class BankPaymentRequest
    {
        public BankPaymentRequest(decimal amount,
                                  Currency currencyCode,
                                  Brand brand,
                                  string expiryMonth,
                                  string expiryYear,
                                  string lastFourDigits,
                                  string cvv)
        {
            this.Amount = amount;
            this.CurrencyCode = currencyCode;
            this.Brand = brand;
            this.ExpiryMonth = expiryMonth;
            this.ExpiryYear = expiryYear;
            this.LastFourDigits = lastFourDigits;
            this.CVV = cvv;
        }

        public decimal Amount { get; set; }
        public Currency CurrencyCode { get; set; }

        public Brand Brand { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string LastFourDigits { get; set; }
        public string CVV { get; set; }
    }
}