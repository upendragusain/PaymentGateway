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
        public PaymentStatus Status { get; private set; }
        public int FailureCode { get; private set; }

        [StringLength(100)]
        public string FailureMesage { get; private set; }

        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; }

        public void AddPaymentResponse(
            Guid id,
            PaymentStatus status,
            int failureCode,
            string failureMesage)
        {
            PaymentResponseId = id;
            Status = status;
            FailureCode = failureCode;
            FailureMesage = failureMesage;
        }
    }

    public class Card : Entity
    {
        protected Card()
        {

        }

        public Card(
            Brand brand,
            byte expiryMonth,
            int expiryYear,
            string lastFourDigits,
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

            if (lastFourDigits.Length > 4)
            {
                throw new PaymentGatewayDomainException($"invalid {nameof(lastFourDigits)}");
            }

            if (cvv <= 0)
            {
                throw new PaymentGatewayDomainException($"invalid {nameof(cvv)}");
            }

            Brand = brand;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            LastFourDigits = lastFourDigits;
            Cvv = cvv;

            Is3DSecure = is3DSecure;
        }

        public Brand Brand { get; private set; }
        public byte ExpiryMonth { get; private set; }
        public int ExpiryYear { get; private set; }

        [StringLength(4)]
        public string LastFourDigits { get; private set; }
        public int Cvv { get; private set; }
        public bool Is3DSecure { get; private set; }

        public Charge Charge { get; private set; }
        public Guid MerchantId { get; set; }
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

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Succeeded,
        Pending,
        Failed
    }
}
