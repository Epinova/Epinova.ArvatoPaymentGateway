namespace Epinova.ArvatoPaymentGateway
{
    public class OrderItem : IIdempotent
    {
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrossUnitPrice { get; set; }
        public string ImageUrl { get; set; }
        public int LineNumber { get; set; }
        public decimal NetUnitPrice { get; set; }
        public string ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal VatAmount { get; set; }
        public double VatPercent { get; set; }

        public int GetIdempotentKey()
        {
            unchecked
            {
                int hashCode = Description?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ DiscountAmount.GetHashCode();
                hashCode = (hashCode * 397) ^ GrossUnitPrice.GetHashCode();
                hashCode = (hashCode * 397) ^ (ImageUrl?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ LineNumber;
                hashCode = (hashCode * 397) ^ NetUnitPrice.GetHashCode();
                hashCode = (hashCode * 397) ^ (ProductId?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ Quantity.GetHashCode();
                hashCode = (hashCode * 397) ^ VatAmount.GetHashCode();
                hashCode = (hashCode * 397) ^ VatPercent.GetHashCode();
                return hashCode;
            }
        }
    }
}
