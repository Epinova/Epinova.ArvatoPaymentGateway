namespace Epinova.ArvatoPaymentGateway
{
    internal class OrderInfoDto
    {
        public CurrencyDto Currency { get; set; }
        public decimal DiscountAmount { get; set; }
        public OrderItemDto[] Items { get; set; }
        public string Number { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }
    }
}
