using System;

namespace Epinova.ArvatoPaymentGateway
{
    internal class ResponseOrderDto
    {
        public CurrencyDto Currency { get; set; }
        public CheckoutCustomerDto Customer { get; set; }
        public bool HasSeparateDeliveryAddress { get; set; }
        public DateTime InsertedAt { get; set; }
        public OrderChannelTypeDto OrderChannelType { get; set; }
        public OrderDeliveryTypeDto OrderDeliveryType { get; set; }
        public Guid OrderId { get; set; }
        public OrderItemExtendedDto[] OrderItems { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatusDto Status { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
