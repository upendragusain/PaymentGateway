using System;
using System.Threading.Tasks;

namespace PaymentGateway.API.Integration.Bank
{
    public class AquiringBankApiService : IAquiringBankApiService
    {
        public async Task<BankPaymentResponse> RequestPayment(
            BankPaymentRequest bankPaymentRequest)
        {
            //simulate the bank
            return await Task.FromResult(new BankPaymentResponse(Guid.NewGuid(), Domain.PaymentStatus.Succeeded, 1, null));
        }
    }
}
