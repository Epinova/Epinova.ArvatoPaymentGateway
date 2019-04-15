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
    }
}