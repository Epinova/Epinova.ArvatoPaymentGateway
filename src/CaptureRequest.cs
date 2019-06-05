namespace Epinova.ArvatoPaymentGateway
{
    public class CaptureRequest : IIdempotent
    {
        public string InvoiceNumber { get; set; }
        public OrderSummary OrderDetails { get; set; }
        public string OrderNumber { get; set; }

        public int GetIdempotentKey()
        {
            unchecked
            {
                int hashCode = InvoiceNumber?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (OrderDetails?.GetIdempotentKey() ?? 0);
                hashCode = (hashCode * 397) ^ (OrderNumber?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}