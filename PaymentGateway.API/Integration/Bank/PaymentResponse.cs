using PaymentGateway.Domain;
using System;

namespace PaymentGateway.API.Integration.Bank
{
    public class PaymentResponse
    {
        public PaymentResponse(Guid id,
                                   string status,
                                   string failureCode)
        {
            PaymentResponseId = id;
            Status = status;
            FailureCode = failureCode;
        }

        public Guid PaymentResponseId { get; private set; }
        public string Status { get; private set; }
        public string FailureCode { get; private set; }
    }
}