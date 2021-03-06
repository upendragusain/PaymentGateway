using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.Exceptions
{
    public class PaymentGatewayDomainException : Exception
    {
        public PaymentGatewayDomainException()
        { }

        public PaymentGatewayDomainException(string message)
            : base(message)
        { }

        public PaymentGatewayDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
