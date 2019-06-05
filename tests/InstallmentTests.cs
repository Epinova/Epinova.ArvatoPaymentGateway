using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class InstallmentTests
    {
        [Fact]
        public void GetHashCode_TwoSimilarInstances_ReturnsSameHashCode()
        {
            var installment1 = new Installment { ProfileNo = 1 };

            var installment2 = new Installment { ProfileNo = 1 };

            Assert.Equal(installment1.GetHashCode(), installment2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_TwoUnlikeInstances_ReturnsDifferentHashCode()
        {
            var installment1 = new Installment { ProfileNo = 1 };

            var installment2 = new Installment { ProfileNo = 2 };

            Assert.NotEqual(installment1.GetHashCode(), installment2.GetHashCode());
        }
    }
}