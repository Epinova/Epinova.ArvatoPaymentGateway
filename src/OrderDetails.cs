using System;

namespace Epinova.ArvatoPaymentGateway
{
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public OrderItem[] OrderItems { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }
    }
}
