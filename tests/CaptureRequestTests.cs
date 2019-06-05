using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class CaptureRequestTests
    {
        [Fact]
        public void GetIdempotentKey_TwoSimilarInstances_ReturnsSameHashCode()
        {
            var request1 = new CaptureRequest
            {
                InvoiceNumber = "1",
                OrderNumber = "2",
                OrderDetails = new OrderSummary
                {
                    Currency = "NOK",
                    DiscountAmount = 3,
                    TotalGrossAmount = 4,
                    TotalNetAmount = 5,
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
                }
            };

            var request2 = new CaptureRequest
            {
                InvoiceNumber = "1",
                OrderNumber = "2",
                OrderDetails = new OrderSummary
                {
                    Currency = "NOK",
                    DiscountAmount = 3,
                    TotalGrossAmount = 4,
                    TotalNetAmount = 5,
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
                }
            };
            Assert.Equal(request1.GetIdempotentKey(), request2.GetIdempotentKey());
        }

        [Fact]
        public void GetIdempotentKey_TwoUnlikeInstances_ReturnsDifferentHashCode()
        {
            var request1 = new CaptureRequest
            {
                InvoiceNumber = "1",
                OrderNumber = "2",
                OrderDetails = new OrderSummary
                {
                    Currency = "NOK",
                    DiscountAmount = 3,
                    TotalGrossAmount = 4,
                    TotalNetAmount = 5,
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
                }
            };

            var request2 = new CaptureRequest
            {
                InvoiceNumber = "11",
                OrderNumber = "2",
                OrderDetails = new OrderSummary
                {
                    Currency = "NOK",
                    DiscountAmount = 3,
                    TotalGrossAmount = 4,
                    TotalNetAmount = 5,
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
                }
            };
            Assert.NotEqual(request1.GetIdempotentKey(), request2.GetIdempotentKey());
        }
    }
}