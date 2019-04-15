namespace Epinova.ArvatoPaymentGateway
{
    public class OrderItem
    {
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrossUnitPrice { get; set; }
        public int LineNumber { get; set; }
        public decimal NetUnitPrice { get; set; }
        public string ProductId { get; set; }
        public decimal Quantity { get; set; }

        public decimal VatAmount { get; set; }

        //public string VatCategory { get; set; }
        public double VatPercent { get; set; }
    }
}