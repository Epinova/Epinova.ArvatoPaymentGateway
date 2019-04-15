using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class CreditRequestTests
    {
        [Fact]
        public void Ctor_LineItems_IsNotNull()
        {
            var request = new CreditRequest();

            Assert.NotNull(request.OrderItems);
        }
    }
}