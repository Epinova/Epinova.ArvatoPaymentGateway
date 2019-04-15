namespace Epinova.ArvatoPaymentGateway
{
    public class OrderSummary
    {
        public string Currency { get; set; }
        public decimal DiscountAmount { get; set; }
        public OrderItem[] Items { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }
    }
}