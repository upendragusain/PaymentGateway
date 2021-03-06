using PaymentGateway.Domain;
using System;

namespace PaymentGateway.API.Integration.Bank
{
    public class BankPaymentResponse
    {
        public BankPaymentResponse(Guid id,
                                   PaymentStatus status,
                                   int failureCode,
                                   string failureMesage)
        {
            PaymentResponseId = id;
            Status = status;
            FailureCode = failureCode;
            FailureMesage = failureMesage;
        }

        public Guid PaymentResponseId { get; private set; }
        public PaymentStatus Status { get; private set; }
        public int FailureCode { get; private set; }
        public string FailureMesage { get; private set; }
    }
}