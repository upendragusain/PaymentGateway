using NUnit.Framework;
using PaymentGateway.Domain;
using PaymentGateway.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace PaymentGateway.Test.Domain
{
    public class ChargeEntityShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCaseSource("EntityTestData")]
        public void ThrowExceptionForInvalidAmount(decimal amount)
        {
            var card = new Card(Brand.AMEX,
                                1,
                                2021,
                                "1234567890123456",
                                123,
                                true);

            Assert.Throws<PaymentGatewayDomainException>(() => new Charge(amount,
                                    Currency.GBP,
                                    card,
                                    Guid.NewGuid()));
        }

        private static IEnumerable<TestCaseData> EntityTestData()
        {
            yield return new TestCaseData(0.00001m)
            .SetName("0.00001");

            yield return new TestCaseData(0m)
           .SetName("0");

            yield return new TestCaseData(999_999_999m)
           .SetName("999_999_999");
        }
    }
}
