using BenchmarkDotNet.Attributes;
using PaymentGateway.Domain;

namespace PaymentGateway.Benchmarks
{
    public class EncryptionBenchmarks
    {
        private static readonly AESEncryptionService encryptionService = new AESEncryptionService();
        
        [Benchmark(Baseline = true)]
        public void Encrypt()
        {
            encryptionService.Encrypt("1234567890123456");
        }
    }
}
