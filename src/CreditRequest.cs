namespace Epinova.ArvatoPaymentGateway
{
    public class CreditRequest : IIdempotent
    {
        public CreditRequest()
        {
            OrderItems = new OrderItem[0];
        }

        public string CaptureNumber { get; set; }
        public string CreditNoteNumber { get; set; }
        public OrderItem[] OrderItems { get; set; }
        public string OrderNumber { get; set; }

        public int GetIdempotentKey()
        {
            unchecked
            {
                int hashCode = CaptureNumber?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (CreditNoteNumber?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ OrderItems.GetIdempotentListKey();
                hashCode = (hashCode * 397) ^ (OrderNumber?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
