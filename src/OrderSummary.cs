namespace Epinova.ArvatoPaymentGateway
{
    public class OrderSummary : IIdempotent
    {
        public OrderSummary()
        {
            Items = new OrderItem[0];
        }

        public string Currency { get; set; }
        public decimal DiscountAmount { get; set; }
        public OrderItem[] Items { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }

        public int GetIdempotentKey()
        {
            unchecked
            {
                int hashCode = Currency?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ DiscountAmount.GetHashCode();
                hashCode = (hashCode * 397) ^ Items.GetIdempotentListKey();
                hashCode = (hashCode * 397) ^ TotalGrossAmount.GetHashCode();
                hashCode = (hashCode * 397) ^ TotalNetAmount.GetHashCode();
                return hashCode;
            }
        }
    }
}