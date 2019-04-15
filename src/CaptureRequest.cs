namespace Epinova.ArvatoPaymentGateway
{
    public class CaptureRequest
    {
        public string InvoiceNumber { get; set; }
        public OrderSummary OrderDetails { get; set; }
        public string OrderNumber { get; set; }
    }
}