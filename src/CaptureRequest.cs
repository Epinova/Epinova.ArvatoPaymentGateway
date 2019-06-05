namespace Epinova.ArvatoPaymentGateway
{
    public class CaptureRequest
    {
        public string InvoiceNumber { get; set; }
        public OrderSummary OrderDetails { get; set; }
        public string OrderNumber { get; set; }

        public override int GetHashCode()
        {
            return CalculateHash();
        }

        private int CalculateHash()
        {
            unchecked
            {
                int hashCode = InvoiceNumber?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (OrderDetails?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (OrderNumber?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}