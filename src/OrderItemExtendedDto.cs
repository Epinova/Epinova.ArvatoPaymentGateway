using System;
using Epinova.ArvatoPaymentGateway.Base;

namespace Epinova.ArvatoPaymentGateway
{
    internal class OrderItemExtendedDto : BaseOrderItemDto
    {
        public DateTime InsertedAt { get; set; }
        public Guid OrderId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
