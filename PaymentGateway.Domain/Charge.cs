using PaymentGateway.Domain.Exceptions;
using System;
using System.Text.Json.Serialization;

namespace PaymentGateway.Domain
{
    public class Charge : Entity
    {
        public Charge(
            decimal amount,
            Currency currency,
            BillingDetail billingDetail,
            Card card,
            Guid idempotencyKey)
        {
            BillingDetail = billingDetail ?? throw new ArgumentNullException(nameof(billingDetail));
            Card = card ?? throw new ArgumentNullException(nameof(card));

            if (amount <= Constants.CHARGE_AMOUNT_MIN || amount >= Constants.CHARGE_AMOUNT_MAX)
                // wonder if it's safe to disclose the limits here?
                throw new PaymentDomainException($"invalid amount");

            Amount = amount;
            Currency = currency;

            IdempotencyKey = idempotencyKey;
        }

        public decimal Amount { get; }
        public Currency Currency { get; }
        public BillingDetail BillingDetail { get; }
        public Card Card { get; }
        public Guid IdempotencyKey { get; }
    }

    public class Card
    {
        public Card(
            Brand brand,
            byte expiryMonth,
            int expiryYear,
            int lastFourDigits,
            int cvv,
            bool is3DSecure)
        {
            Brand = brand;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            LastFourDigits = lastFourDigits;
            Cvv = cvv;
            Is3DSecure = is3DSecure;
        }

        public Brand Brand { get; }
        public byte ExpiryMonth { get; }
        public int ExpiryYear { get; }
        public int LastFourDigits { get; }
        public int Cvv { get; }
        public bool Is3DSecure { get; }
    }

    public class BillingDetail
    {
        public BillingDetail(
            string email,
            string name,
            string phone,
            Address address)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or empty.", nameof(email));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentException($"'{nameof(phone)}' cannot be null or empty.", nameof(phone));
            }

            Email = email;
            Name = name;
            Phone = phone;
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public string Email { get; }
        public string Name { get; }
        public string Phone { get; }
        public Address Address { get; }
    }

    public class Address
    {
        public Address(
            string city,
            string country,
            string line1,
            string line2,
            string postalCode,
            string state)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException($"'{nameof(city)}' cannot be null or whitespace.", nameof(city));
            }

            City = city;
            Country = country;
            Line1 = line1;
            Line2 = line2;
            PostalCode = postalCode;
            State = state;
        }

        public string City { get; }
        public string Country { get; }
        public string Line1 { get; }
        public string Line2 { get; }
        public string PostalCode { get; }
        public string State { get; }
    }

    public class PaymentResponse
    {
        public PaymentResponse(
            PaymentStatus status,
            int failureCode,
            string failureMesage)
        {
            Status = status;
            FailureCode = failureCode;
            FailureMesage = failureMesage;
        }

        public PaymentStatus Status { get; }
        public int FailureCode { get; }
        public string FailureMesage { get; }
    }

    public abstract class Entity
    {
        public Guid Id { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }
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
        MASTERCARD
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Succeeded,
        Pending,
        Failed
    }
}
