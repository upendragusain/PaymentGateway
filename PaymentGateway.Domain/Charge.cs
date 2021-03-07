using PaymentGateway.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PaymentGateway.Domain
{
    public class Charge : Entity
    {
        protected Charge()
        {

        }

        public Charge(
            decimal amount,
            Currency currency,
            Card card,
            Guid idempotencyKey,
            Guid merchantId)
        {
            Card = card ?? throw new ArgumentNullException(nameof(card));

            if (amount <= Constants.CHARGE_AMOUNT_MIN || amount >= Constants.CHARGE_AMOUNT_MAX)
                // wonder if it's safe to disclose the limits here?
                throw new PaymentGatewayDomainException($"invalid {nameof(amount)}");

            Amount = amount;
            Currency = currency;

            MerchantId = merchantId;
            IdempotencyKey = idempotencyKey;

            Id = Guid.NewGuid();
            DateTimeCreated = DateTime.UtcNow;
            DateTimeUpdated = null;
        }

        [Column(TypeName = "decimal(19, 4)")]
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        // can only add card against a charge using the charge constructor
        public Card Card { get; private set; }

        public Guid IdempotencyKey { get; private set; }

        public Guid PaymentResponseId { get; private set; }
        public string Status { get; private set; }
        public string FailureCode { get; private set; }

        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; }

        public void AddPaymentResponse(
            Guid id,
            string status,
            string failureCode)
        {
            //todo: make sure status and failurecode agree!
            PaymentResponseId = id;
            Status = status;
            FailureCode = failureCode;
        }
    }

    public class Card : Entity
    {
        protected Card()
        {
            _charges = new List<Charge>();
        }

        public Card(
            Brand brand,
            byte expiryMonth,
            int expiryYear,
            string number,
            int cvv,
            bool is3DSecure)
        {
            if (expiryMonth <= 0 || expiryMonth > 12)
            {
                throw new PaymentGatewayDomainException($"invalid {nameof(expiryMonth)}");
            }

            if (expiryYear < DateTime.UtcNow.Year)// todo: more logic here for around midnight dec 31!
            {
                throw new PaymentGatewayDomainException($"invalid {nameof(ExpiryYear)}");
            }

            if (number.Length < 12 || number.Length > 19)
            {
                throw new PaymentGatewayDomainException($"invalid {nameof(number)}");
            }

            if (cvv <= 0)
            {
                throw new PaymentGatewayDomainException($"invalid {nameof(cvv)}");
            }

            Brand = brand;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            Number = number;
            Cvv = cvv;

            Is3DSecure = is3DSecure;

            Id = Guid.NewGuid();
            DateTimeCreated = DateTime.UtcNow;
            DateTimeUpdated = null;

        }

        public Brand Brand { get; private set; }
        public byte ExpiryMonth { get; private set; }
        public int ExpiryYear { get; private set; }

        public string Number { get; private set; }
        public int Cvv { get; private set; }
        public bool Is3DSecure { get; private set; }

        private readonly List<Charge> _charges;
        public IReadOnlyCollection<Charge> Charges => _charges;

        public void Encrypt(IEncryptionService encryptionService)
        {
            Number = Convert.ToBase64String(encryptionService.Encrypt(Number));
        }

        public void Decrypt(IEncryptionService encryptionService)
        {
            Number = encryptionService.Decrypt(Convert.FromBase64String(Number));
        }
    }

    public class Merchant : Entity
    {
        protected Merchant()
        {
            _charges = new List<Charge>();
        }

        public Merchant(
            string name,
            string postalCode)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
            PostalCode = string.IsNullOrWhiteSpace(PostalCode) ? throw new ArgumentNullException(nameof(PostalCode)) : PostalCode;
        }

        [StringLength(300)]
        public string Name { get; private set; }

        [StringLength(10)]
        public string PostalCode { get; private set; }

        private readonly List<Charge> _charges;
        public IReadOnlyCollection<Charge> Charges => _charges;
    }

    public abstract class Entity
    {
        public Entity()
        {
        }

        public Guid Id { get; protected set; }

        public DateTime DateTimeCreated { get; protected set; }

        public DateTime? DateTimeUpdated { get; protected set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Currency
    {
        GBP,
        USD
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Brand
    {
        VISA,
        MASTERCARD,
        AMEX
    }

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    //public enum PaymentStatus
    //{
    //    Succeeded,
    //    Pending,
    //    Failed
    //}

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    //public enum FailureReason
    //{
    //    none = 0,
    //    account_number_invalid,
    //    amount_too_large,
    //    authentication_required,
    //    card_decline_rate_limit_exceeded,
    //    country_code_invalid,
    //    expired_card,
    //    invalid_cvc
    //}
}
