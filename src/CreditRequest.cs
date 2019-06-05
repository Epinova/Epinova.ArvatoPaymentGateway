namespace Epinova.ArvatoPaymentGateway
{
    public class CreditRequest
    {
        public CreditRequest()
        {
            OrderItems = new OrderItem[0];
        }

        public string CaptureNumber { get; set; }
        public string CreditNoteNumber { get; set; }
        public OrderItem[] OrderItems { get; set; }
        public string OrderNumber { get; set; }

        public override int GetHashCode()
        {
            return CalculateHash();
        }

        private int CalculateHash()
        {
            unchecked
            {
                int hashCode = CaptureNumber?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (CreditNoteNumber?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ OrderItems.GetListHashCode();
                hashCode = (hashCode * 397) ^ (OrderNumber?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}