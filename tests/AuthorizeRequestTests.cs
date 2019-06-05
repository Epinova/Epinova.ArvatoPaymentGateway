using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class AuthorizeRequestTests
    {
        [Fact]
        public void GetIdempotentKey_TwoSimilarInstances_ReturnsSameHashCode()
        {
            var request1 = new AuthorizeRequest
            {
                Customer = new AuthorizeCustomer
                {
                    Address = "address",
                    City = "city",
                    Email = "email",
                    FirstName = "first name",
                    Identifier = "identifier",
                    LastName = "last name",
                    Phone = "phone",
                    PostalCode = "postal code",
                    PostalPlace = "postal place"
                },
                Order = new OrderInfo
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
                },
                Payment = new Payment
                {
                    Account = new AccountProduct { ProfileNo = 1 },
                    Installment = new Installment { ProfileNo = 2 },
                    Type = PaymentType.Invoice
                },
            };

            var request2 = new AuthorizeRequest
            {
                Customer = new AuthorizeCustomer
                {
                    Address = "address",
                    City = "city",
                    Email = "email",
                    FirstName = "first name",
                    Identifier = "identifier",
                    LastName = "last name",
                    Phone = "phone",
                    PostalCode = "postal code",
                    PostalPlace = "postal place"
                },
                Order = new OrderInfo
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
                },
                Payment = new Payment
                {
                    Account = new AccountProduct { ProfileNo = 1 },
                    Installment = new Installment { ProfileNo = 2 },
                    Type = PaymentType.Invoice
                },
            };
            Assert.Equal(request1.GetIdempotentKey(), request2.GetIdempotentKey());
        }

        [Fact]
        public void GetIdempotentKey_TwoUnlikeInstances_ReturnsDifferentHashCode()
        {
            var request1 = new AuthorizeRequest
            {
                Customer = new AuthorizeCustomer
                {
                    Address = "address",
                    City = "city",
                    Email = "email",
                    FirstName = "first name",
                    Identifier = "identifier",
                    LastName = "last name",
                    Phone = "phone",
                    PostalCode = "postal code",
                    PostalPlace = "postal place"
                },
                Order = new OrderInfo
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
                },
                Payment = new Payment
                {
                    Account = new AccountProduct { ProfileNo = 1 },
                    Installment = new Installment { ProfileNo = 2 },
                    Type = PaymentType.Invoice
                },
            };

            var request2 = new AuthorizeRequest
            {
                Customer = new AuthorizeCustomer
                {
                    Address = "address2",
                    City = "city2",
                    Email = "email2",
                    FirstName = "first name2",
                    Identifier = "identifier2",
                    LastName = "last name",
                    Phone = "phone",
                    PostalCode = "postal code",
                    PostalPlace = "postal place"
                },
                Order = new OrderInfo
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
                },
                Payment = new Payment
                {
                    Account = new AccountProduct { ProfileNo = 1 },
                    Installment = new Installment { ProfileNo = 2 },
                    Type = PaymentType.Invoice
                },
            };
            Assert.NotEqual(request1.GetIdempotentKey(), request2.GetIdempotentKey());
        }
    }
}