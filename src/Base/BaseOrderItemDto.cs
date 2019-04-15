namespace Epinova.ArvatoPaymentGateway.Base
{
    internal abstract class BaseOrderItemDto
    {
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrossUnitPrice { get; set; }
        public string GroupId { get; set; }
        public int LineNumber { get; set; }
        public string MarketPlaceSellerId { get; set; }
        public decimal NetUnitPrice { get; set; }
        public string ProductId { get; set; }
        public string ProductUrl { get; set; }
        public decimal Quantity { get; set; }
        public string UnitCode { get; set; }

        public decimal VatAmount { get; set; }

        //public VatCategoryDto VatCategory { get; set; }
        public decimal VatPercent { get; set; }
    }
}