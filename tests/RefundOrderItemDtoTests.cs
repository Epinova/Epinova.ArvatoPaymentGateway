using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class RefundOrderItemDtoTests
    {
        [Fact]
        public void Ctor_RefundType_IsRefund()
        {
            var dto = new RefundOrderItemDto();
            Assert.Equal(RefundTypeDto.Refund, dto.RefundType);
        }
    }
}
