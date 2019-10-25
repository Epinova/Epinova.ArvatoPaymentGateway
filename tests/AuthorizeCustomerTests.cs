using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class AuthorizeCustomerTests
    {
        [Fact]
        public void GetIdempotentKey_TwoSimilarInstances_ReturnsSameHashCode()
        {
            var customer1 = new AuthorizeCustomer
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
            };

            var customer2 = new AuthorizeCustomer
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
            };
            Assert.Equal(customer1.GetIdempotentKey(), customer2.GetIdempotentKey());
        }

        [Fact]
        public void GetIdempotentKey_TwoUnlikeInstances_ReturnsDifferentHashCode()
        {
            var customer1 = new AuthorizeCustomer
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
            };

            var customer2 = new AuthorizeCustomer
            {
                Address = "address 2",
                City = "city 2",
                Email = "email 2",
                FirstName = "first name 2",
                Identifier = "identifier 2",
                LastName = "last name 2",
                Phone = "phone 2",
                PostalCode = "postal code 2",
                PostalPlace = "postal place 2"
            };
            Assert.NotEqual(customer1.GetIdempotentKey(), customer2.GetIdempotentKey());
        }

        [Fact]
        public void ToLoggableString_CustomerWithInfo_DisplayNameEmailAndMaskedIdentifier()
        {
            var customer = new AuthorizeCustomer
            {
                FirstName = Factory.GetString(),
                LastName = Factory.GetString(),
                Email = Factory.GetString(),
                Identifier = "00000011111"
            };
            Assert.Equal($"Name '{customer.FirstName} {customer.LastName}', email: '{customer.Email}', identifier: '000000XXXXX'", customer.ToLoggableString());
        }

        [Fact]
        public void ToLoggableString_CustomerWithShortIdentifier_DisplayNameEmailAndRawIdentifier()
        {
            var customer = new AuthorizeCustomer
            {
                FirstName = Factory.GetString(),
                LastName = Factory.GetString(),
                Email = Factory.GetString(),
                Identifier = "000000"
            };
            Assert.Equal($"Name '{customer.FirstName} {customer.LastName}', email: '{customer.Email}', identifier: '000000'", customer.ToLoggableString());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ToLoggableString_IdentifierIsNullOrWhite_DisplayAsIs(string identifier)
        {
            var customer = new AuthorizeCustomer { FirstName = Factory.GetString(), LastName = Factory.GetString(), Identifier = identifier };
            Assert.Equal($"Name '{customer.FirstName} {customer.LastName}', email: '{customer.Email}', identifier: '{identifier}'", customer.ToLoggableString());
        }
    }
}
