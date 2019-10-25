using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class OrderSummaryTests
    {
        [Fact]
        public void Ctor_Items_IsNotNull()
        {
            var request = new OrderSummary();

            Assert.NotNull(request.Items);
        }

        [Fact]
        public void GetIdempotentKey_TwoSimilarInstances_ReturnsSameHashCode()
        {
            var summary1 = new OrderSummary
            {
                Currency = "NOK",
                DiscountAmount = 1,
                TotalGrossAmount = 2,
                TotalNetAmount = 3,
                Items = new[]
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

            var summary2 = new OrderSummary
            {
                Currency = "NOK",
                DiscountAmount = 1,
                TotalGrossAmount = 2,
                TotalNetAmount = 3,
                Items = new[]
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
            Assert.Equal(summary1.GetIdempotentKey(), summary2.GetIdempotentKey());
        }

        [Fact]
        public void GetIdempotentKey_TwoUnlikeInstances_ReturnsDifferentHashCode()
        {
            var summary1 = new OrderSummary
            {
                Currency = "NOK",
                DiscountAmount = 1,
                TotalGrossAmount = 2,
                TotalNetAmount = 3,
                Items = new[]
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

            var summary2 = new OrderSummary
            {
                Currency = "NOK",
                DiscountAmount = 11,
                TotalGrossAmount = 22,
                TotalNetAmount = 33,
                Items = new[]
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
            Assert.NotEqual(summary1.GetIdempotentKey(), summary2.GetIdempotentKey());
        }
    }
}
