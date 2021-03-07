using System.Threading.Tasks;

namespace PaymentGateway.API.Integration.Bank
{
    public interface IAquiringBankApiService
    {
        Task<PaymentResponse> RequestPayment(BankPaymentRequest bankPaymentRequest);
    }
}