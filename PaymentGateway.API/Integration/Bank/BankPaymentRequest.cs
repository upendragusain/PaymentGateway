using PaymentGateway.Domain;

namespace PaymentGateway.API.Integration.Bank
{
    public class BankPaymentRequest
    {
        public BankPaymentRequest(decimal amount,
                                  Currency currencyCode,
                                  Brand brand,
                                  byte expiryMonth,
                                  int expiryYear,
                                  string lastFourDigits,
                                  int cvv)
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
        public byte ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string LastFourDigits { get; set; }
        public int CVV { get; set; }
    }
}