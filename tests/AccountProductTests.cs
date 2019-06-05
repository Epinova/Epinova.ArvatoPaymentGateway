using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class AccountProductTests
    {
        [Fact]
        public void GetHashCode_TwoSimilarInstances_ReturnsSameHashCode()
        {
            var product1 = new AccountProduct { ProfileNo = 1 };

            var product2 = new AccountProduct { ProfileNo = 1 };

            Assert.Equal(product1.GetHashCode(), product2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_TwoUnlikeInstances_ReturnsDifferentHashCode()
        {
            var product1 = new AccountProduct { ProfileNo = 1 };

            var product2 = new AccountProduct { ProfileNo = 2 };

            Assert.NotEqual(product1.GetHashCode(), product2.GetHashCode());
        }
    }
}