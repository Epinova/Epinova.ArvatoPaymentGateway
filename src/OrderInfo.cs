using System.Collections.Generic;

namespace Epinova.ArvatoPaymentGateway
{
    public class OrderInfo
    {
        public OrderInfo()
        {
            LineItems = new List<OrderItem>();
        }

        public string Currency { get; set; }
        public decimal DiscountAmount { get; set; }
        public List<OrderItem> LineItems { get; set; }
        public string Number { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }
    }
}