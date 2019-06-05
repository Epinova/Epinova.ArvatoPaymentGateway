using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class AuthorizeCustomerTests
    {
        [Fact]
        public void GetHashCode_TwoSimilarInstances_ReturnsSameHashCode()
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
            Assert.Equal(customer1.GetHashCode(), customer2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_TwoUnlikeInstances_ReturnsDifferentHashCode()
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
            Assert.NotEqual(customer1.GetHashCode(), customer2.GetHashCode());
        }
    }
}