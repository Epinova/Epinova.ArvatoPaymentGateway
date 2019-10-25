namespace Epinova.ArvatoPaymentGateway
{
    public class OrderResponse
    {
        public Cancellation[] Cancellations { get; set; }
        public Capture[] Captures { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public Refund[] Refunds { get; set; }
    }
}
