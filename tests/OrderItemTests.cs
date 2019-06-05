using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class OrderItemTests
    {
        [Fact]
        public void GetHashCode_TwoSimilarInstances_ReturnsSameHashCode()
        {
            var item1 =
                new OrderItem
                {
                    Description = "desc",
                    DiscountAmount = 1,
                    GrossUnitPrice = 2,
                    LineNumber = 3,
                    NetUnitPrice = 4,
                    ProductId = "pid",
                    Quantity = 5,
                    VatAmount = 6,
                    VatPercent = 7
                };

            var item2 = new OrderItem
            {
                Description = "desc",
                DiscountAmount = 1,
                GrossUnitPrice = 2,
                LineNumber = 3,
                NetUnitPrice = 4,
                ProductId = "pid",
                Quantity = 5,
                VatAmount = 6,
                VatPercent = 7
            };
            Assert.Equal(item1.GetHashCode(), item2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_TwoUnlikeInstances_ReturnsDifferentHashCode()
        {
            var item1 =
                new OrderItem
                {
                    Description = "desc",
                    DiscountAmount = 1,
                    GrossUnitPrice = 2,
                    LineNumber = 3,
                    NetUnitPrice = 4,
                    ProductId = "pid",
                    Quantity = 5,
                    VatAmount = 6,
                    VatPercent = 7
                };

            var item2 = new OrderItem
            {
                Description = "desc2",
                DiscountAmount = 11,
                GrossUnitPrice = 22,
                LineNumber = 33,
                NetUnitPrice = 44,
                ProductId = "pid2",
                Quantity = 55,
                VatAmount = 66,
                VatPercent = 77
            };
            Assert.NotEqual(item1.GetHashCode(), item2.GetHashCode());
        }
    }
}