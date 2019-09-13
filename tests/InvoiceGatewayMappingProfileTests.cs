using System;
using System.Linq;
using AutoMapper;
using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class InvoiceGatewayMappingProfileTests
    {
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        public InvoiceGatewayMappingProfileTests()
        {
            _config = new MapperConfiguration(cfg => { cfg.AddProfile<InvoiceGatewayMappingProfile>(); });
            _mapper = _config.CreateMapper();
        }

        [Fact]
        public void AllowNullCollections_IsFalse()
        {
            var profile = new InvoiceGatewayMappingProfile();
            Assert.False(profile.AllowNullCollections);
        }

        [Fact]
        public void AutoMapperConfiguration_IsValid()
        {
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_AuthorizeRequestDto_CorrectCustomerIdentificationNumber()
        {
            var src = new AuthorizeRequest { Customer = new AuthorizeCustomer { Identifier = Factory.GetString() } };

            var dest = _mapper.Map<AuthorizeRequestDto>(src);

            Assert.Equal(src.Customer.Identifier, dest.Customer.IdentificationNumber);
        }

        [Fact]
        public void Map_AuthorizeRequestDto_CorrectCustomerMobile()
        {
            var src = new AuthorizeRequest { Customer = new AuthorizeCustomer { Phone = Factory.GetString() } };

            var dest = _mapper.Map<AuthorizeRequestDto>(src);

            Assert.Equal(src.Customer.Phone, dest.Customer.MobilePhone);
        }

        [Fact]
        public void Map_AuthorizeRequestDto_CorrectOrderNumber()
        {
            var src = new AuthorizeRequest { Order = new OrderInfo { Number = Factory.GetString() } };

            var dest = _mapper.Map<AuthorizeRequestDto>(src);

            Assert.Equal(src.Order.Number, dest.Order.Number);
        }

        [Fact]
        public void Map_AuthorizeResponseDto_CorrectCheckOutId()
        {
            var src = new AuthorizeResponseDto { CheckoutId = Factory.GetString() };

            var dest = _mapper.Map<AuthorizeResponse>(src);

            Assert.Equal(src.CheckoutId, dest.CheckoutId);
        }

        [Fact]
        public void Map_AuthorizeResponseDto_CorrectCustomerNumber()
        {
            var src = new AuthorizeResponseDto { Customer = new CustomerResponseDto { CustomerNumber = Factory.GetString() } };

            var dest = _mapper.Map<AuthorizeResponse>(src);

            Assert.Equal(src.Customer.CustomerNumber, dest.CustomerNumber);
        }

        [Fact]
        public void Map_CaptureResponseDto_CorrectUnauthorizedAmount()
        {
            var src = new CaptureResponseDto { RemainingAuthorizedAmount = Factory.GetInteger() };

            var dest = _mapper.Map<CaptureResponse>(src);

            Assert.Equal(src.RemainingAuthorizedAmount, dest.UnauthorizedAmount);
        }

        [Fact]
        public void Map_CreditRequestDto_CorrectCaptureNumber()
        {
            var src = new CreditRequest { CaptureNumber = Factory.GetString() };

            var dest = _mapper.Map<RefundOrderRequestDto>(src);

            Assert.Equal(src.CaptureNumber, dest.CaptureNumber);
        }

        [Fact]
        public void Map_CreditRequestDto_CorrectLineDescription()
        {
            var src = new CreditRequest
            {
                OrderItems = new[]
                {
                    new OrderItem { Description = Factory.GetString() },
                    new OrderItem { Description = Factory.GetString() }
                }
            };

            var dest = _mapper.Map<RefundOrderRequestDto>(src);

            Assert.Collection(
                dest.OrderItems,
                line => Assert.Equal(src.OrderItems[0].Description, line.Description),
                line => Assert.Equal(src.OrderItems[1].Description, line.Description));
        }

        [Fact]
        public void Map_CreditRequestDto_CorrectLineGrossUnitPrice()
        {
            var src = new CreditRequest
            {
                OrderItems = new[]
                {
                    new OrderItem { GrossUnitPrice = Factory.GetInteger() },
                    new OrderItem { GrossUnitPrice = Factory.GetInteger() }
                }
            };

            var dest = _mapper.Map<RefundOrderRequestDto>(src);

            Assert.Collection(
                dest.OrderItems,
                line => Assert.Equal(src.OrderItems[0].GrossUnitPrice, line.GrossUnitPrice),
                line => Assert.Equal(src.OrderItems[1].GrossUnitPrice, line.GrossUnitPrice));
        }

        [Fact]
        public void Map_CreditResponseDto_CorrectCreditNumbers()
        {
            var src = new RefundOrderResponseDto { RefundNumbers = new[] { Factory.GetString(), Factory.GetString() } };

            var dest = _mapper.Map<CreditResponse>(src);

            Assert.Equal(src.RefundNumbers, dest.CreditNumbers);
        }

        [Fact]
        public void Map_CreditResponseDtoHasNoRefundNumbers_CreditNumbersNotNull()
        {
            var src = new RefundOrderResponseDto { RefundNumbers = null };

            var dest = _mapper.Map<CreditResponse>(src);

            Assert.NotNull(dest.CreditNumbers);
        }

        [Fact]
        public void Map_CustomerLookupResponse_CorrectCustomerCity()
        {
            var src = new CustomerLookupResponseDto
            {
                UserProfiles = new[]
                {
                    new UserProfileDto
                    {
                        FirstName = Factory.GetString(),
                        AddressList = new[]
                        {
                            new AddressDto
                            {
                                City = Factory.GetString()
                            }
                        }
                    }
                }
            };

            var dest = _mapper.Map<CustomerLookupResponse>(src.UserProfiles.FirstOrDefault());

            Assert.Equal(src.UserProfiles.First().AddressList.First().City, dest.City);
        }

        [Fact]
        public void Map_CustomerLookupResponse_CorrectCustomerEmail()
        {
            var src = new CustomerLookupResponseDto { UserProfiles = new[] { new UserProfileDto { Email = Factory.GetString() } } };

            var dest = _mapper.Map<CustomerLookupResponse>(src.UserProfiles.FirstOrDefault());

            Assert.Equal(src.UserProfiles.First().Email, dest.Email);
        }

        [Fact]
        public void Map_CustomerLookupResponse_CorrectCustomerFirstName()
        {
            var src = new CustomerLookupResponseDto { UserProfiles = new[] { new UserProfileDto { FirstName = Factory.GetString() } } };

            var dest = _mapper.Map<CustomerLookupResponse>(src.UserProfiles.FirstOrDefault());

            Assert.Equal(src.UserProfiles.First().FirstName, dest.FirstName);
        }

        [Fact]
        public void Map_CustomerLookupResponse_CorrectCustomerLastName()
        {
            var src = new CustomerLookupResponseDto { UserProfiles = new[] { new UserProfileDto { LastName = Factory.GetString() } } };

            var dest = _mapper.Map<CustomerLookupResponse>(src.UserProfiles.FirstOrDefault());

            Assert.Equal(src.UserProfiles.First().LastName, dest.LastName);
        }

        [Fact]
        public void Map_CustomerLookupResponse_CorrectCustomerMobileNumber()
        {
            var src = new CustomerLookupResponseDto { UserProfiles = new[] { new UserProfileDto { MobileNumber = Factory.GetString() } } };

            var dest = _mapper.Map<CustomerLookupResponse>(src.UserProfiles.FirstOrDefault());

            Assert.Equal(src.UserProfiles.First().MobileNumber, dest.MobileNumber);
        }

        [Fact]
        public void Map_CustomerLookupResponseDtoWithNullAddressInfo_CityIsNull()
        {
            var src = new CustomerLookupResponseDto
            {
                UserProfiles = new[]
                {
                    new UserProfileDto
                    {
                        FirstName = Factory.GetString(),
                        AddressList = null
                    }
                }
            };

            var dest = _mapper.Map<CustomerLookupResponse>(src.UserProfiles.FirstOrDefault());

            Assert.Null(dest.City);
        }

        [Fact]
        public void Map_CustomerLookupResponseDtoWithoutAddressInfo_CityIsNull()
        {
            var src = new CustomerLookupResponseDto
            {
                UserProfiles = new[]
                {
                    new UserProfileDto
                    {
                        FirstName = Factory.GetString(),
                        AddressList = new AddressDto[0]
                    }
                }
            };

            var dest = _mapper.Map<CustomerLookupResponse>(src.UserProfiles.FirstOrDefault());

            Assert.Null(dest.City);
        }

        [Fact]
        public void Map_OrderItemDto_CorrectGrossUnitPrice()
        {
            var src = new OrderItem { GrossUnitPrice = Factory.GetInteger() };

            var dest = _mapper.Map<OrderItemDto>(src);

            Assert.Equal(dest.GrossUnitPrice, src.GrossUnitPrice);
        }

        [Fact]
        public void Map_OrderItemExtendedDto_CorrectGrossUnitPrice()
        {
            var src = new OrderItem { GrossUnitPrice = Factory.GetInteger() };

            var dest = _mapper.Map<OrderItemExtendedDto>(src);

            Assert.Equal(dest.GrossUnitPrice, src.GrossUnitPrice);
        }

        [Fact]
        public void Map_OrderResponse_CorrectOrderId()
        {
            var src = new OrderResponseDto { OrderDetails = new ResponseOrderDto { OrderId = Guid.NewGuid() } };

            var dest = _mapper.Map<OrderResponse>(src);

            Assert.Equal(src.OrderDetails.OrderId, dest.OrderDetails.OrderId);
        }

        [Fact]
        public void Map_OrderResponse_CorrectOrderItemLineNumber()
        {
            int lineNumber = Factory.GetInteger();
            var src = new OrderResponseDto { OrderDetails = new ResponseOrderDto { OrderItems = new[] { new OrderItemExtendedDto { LineNumber = lineNumber } } } };

            var dest = _mapper.Map<OrderResponse>(src);

            Assert.Equal(lineNumber, dest.OrderDetails.OrderItems.First().LineNumber);
        }

        [Fact]
        public void Map_OrderResponse_CorrectOrderItemPrice()
        {
            decimal price = Factory.GetInteger();
            var src = new OrderResponseDto { OrderDetails = new ResponseOrderDto { OrderItems = new[] { new OrderItemExtendedDto { GrossUnitPrice = price } } } };

            var dest = _mapper.Map<OrderResponse>(src);

            Assert.Equal(price, dest.OrderDetails.OrderItems.First().GrossUnitPrice);
        }
    }
}