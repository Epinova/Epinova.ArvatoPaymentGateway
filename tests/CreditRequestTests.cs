using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class CreditRequestTests
    {
        [Fact]
        public void Ctor_OrderItems_IsNotNull()
        {
            var request = new CreditRequest();

            Assert.NotNull(request.OrderItems);
        }

        [Fact]
        public void GetIdempotentKey_TwoSimilarInstances_ReturnsSameHashCode()
        {
            var request1 = new CreditRequest
            {
                CaptureNumber = "1",
                CreditNoteNumber = "2",
                OrderNumber = "3",
                OrderItems = new[]
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

            var request2 = new CreditRequest
            {
                CaptureNumber = "1",
                CreditNoteNumber = "2",
                OrderNumber = "3",
                OrderItems = new[]
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
            Assert.Equal(request1.GetIdempotentKey(), request2.GetIdempotentKey());
        }

        [Fact]
        public void GetIdempotentKey_TwoUnlikeInstances_ReturnsDifferentHashCode()
        {
            var request1 = new CreditRequest
            {
                CaptureNumber = "1",
                CreditNoteNumber = "2",
                OrderNumber = "3",
                OrderItems = new[]
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

            var request2 = new CreditRequest
            {
                CaptureNumber = "11",
                CreditNoteNumber = "2",
                OrderNumber = "3",
                OrderItems = new[]
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
            Assert.NotEqual(request1.GetIdempotentKey(), request2.GetIdempotentKey());
        }
    }
}
