using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public interface IChargeRepository
    {
        Task<int> Create(Charge charge);

        Task<Charge> Get(Guid merchantId, Guid id);

        Task<IEnumerable<Charge>> GetList(Guid merchantId, DateTime date);
    }
}
