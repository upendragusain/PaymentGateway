﻿using PaymentGateway.Domain;
using System;
using MediatR;

namespace PaymentGateway.API.Application.Queries
{
    public class ChargeQuery : IRequest<Charge>
    {
        public Guid MerchantId { get; set; }

        public Guid Id { get; set; }
    }
}