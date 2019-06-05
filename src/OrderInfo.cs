namespace Epinova.ArvatoPaymentGateway
{
    public class OrderInfo : IIdempotent
    {
        public OrderInfo()
        {
            LineItems = new OrderItem[0];
        }

        public string Currency { get; set; }
        public decimal DiscountAmount { get; set; }
        public OrderItem[] LineItems { get; set; }
        public string Number { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }

        public int GetIdempotentKey()
        {
            unchecked
            {
                int hashCode = Currency?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ DiscountAmount.GetHashCode();
                hashCode = (hashCode * 397) ^ LineItems.GetIdempotentListKey();
                hashCode = (hashCode * 397) ^ (Number?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ TotalGrossAmount.GetHashCode();
                hashCode = (hashCode * 397) ^ TotalNetAmount.GetHashCode();
                return hashCode;
            }
        }
    }
}