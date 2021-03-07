using BenchmarkDotNet.Running;

namespace PaymentGateway.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<EncryptionBenchmarks>();
        }
    }
}
