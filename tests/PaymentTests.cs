using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class PaymentTests
    {
        [Fact]
        public void GetHashCode_TwoSimilarInstances_ReturnsSameHashCode()
        {
            var payment1 = new Payment
            {
                Account = new AccountProduct { ProfileNo = 1 },
                Installment = new Installment { ProfileNo = 2 },
                Type = PaymentType.Invoice
            };

            var payment2 = new Payment
            {
                Account = new AccountProduct { ProfileNo = 1 },
                Installment = new Installment { ProfileNo = 2 },
                Type = PaymentType.Invoice
            };
            Assert.Equal(payment1.GetHashCode(), payment2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_TwoUnlikeInstances_ReturnsDifferentHashCode()
        {
            var payment1 = new Payment
            {
                Account = new AccountProduct { ProfileNo = 1 },
                Installment = new Installment { ProfileNo = 2 },
                Type = PaymentType.Invoice
            };

            var payment2 = new Payment
            {
                Account = new AccountProduct { ProfileNo = 3 },
                Installment = new Installment { ProfileNo = 4 },
                Type = PaymentType.Invoice
            };
            Assert.NotEqual(payment1.GetHashCode(), payment2.GetHashCode());
        }
    }
}