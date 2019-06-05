using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class OrderInfoTests
    {
        [Fact]
        public void Ctor_LineItems_IsNotNull()
        {
            var instance = new OrderInfo();
            Assert.NotNull(instance.LineItems);
        }

        [Fact]
        public void GetIdempotentKey_TwoSimilarInstances_ReturnsSameHashCode()
        {
            var order1 = new OrderInfo
            {
                Currency = "NOK",
                DiscountAmount = 1,
                LineItems = new[]
                {
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
                    }
                }
            };

            var order2 = new OrderInfo
            {
                Currency = "NOK",
                DiscountAmount = 1,
                LineItems = new[]
                {
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
                    }
                }
            };
            Assert.Equal(order1.GetIdempotentKey(), order2.GetIdempotentKey());
        }

        [Fact]
        public void GetIdempotentKey_TwoUnlikeInstances_ReturnsDifferentHashCode()
        {
            var order1 = new OrderInfo
            {
                Currency = "NOK",
                DiscountAmount = 1,
                LineItems = new[]
                {
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
                    }
                }
            };

            var order2 = new OrderInfo
            {
                Currency = "SEK",
                DiscountAmount = 2,
                LineItems = new[]
                {
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
                    }
                }
            };
            Assert.NotEqual(order1.GetIdempotentKey(), order2.GetIdempotentKey());
        }
    }
}