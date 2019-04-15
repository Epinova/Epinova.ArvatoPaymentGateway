namespace Epinova.ArvatoPaymentGateway
{
    internal class RefundOrderItemDto : OrderItemDto
    {
        public RefundTypeDto RefundType { get; set; }
    }
}