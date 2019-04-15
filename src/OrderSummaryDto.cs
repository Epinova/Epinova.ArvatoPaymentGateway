namespace Epinova.ArvatoPaymentGateway
{
    internal class OrderSummaryDto
    {
        public decimal DiscountAmount { get; set; }
        public OrderItemDto[] Items { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }
    }
}