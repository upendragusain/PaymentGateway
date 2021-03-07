using System;
using System.Threading.Tasks;

namespace PaymentGateway.API.Integration.Bank
{
    public class AquiringBankApiService : IAquiringBankApiService
    {
        public async Task<PaymentResponse> RequestPayment(
            BankPaymentRequest bankPaymentRequest)
        {
            //simulate the bank
            return await Task.FromResult(
                new PaymentResponse(Guid.NewGuid(), 
                "Succeeded", 
                null));
        }
    }
}
