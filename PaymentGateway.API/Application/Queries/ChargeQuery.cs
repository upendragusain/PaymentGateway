using PaymentGateway.Domain;
using System;
using MediatR;

namespace PaymentGateway.API.Application.Queries
{
    public class ChargeQuery : IRequest<PaymentDetail>
    {
        public Guid Id { get; set; }
    }
}
