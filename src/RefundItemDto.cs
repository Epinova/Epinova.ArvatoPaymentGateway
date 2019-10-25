using System;
using Epinova.ArvatoPaymentGateway.Base;

namespace Epinova.ArvatoPaymentGateway
{
    internal class RefundItemDto : BaseOrderItemDto
    {
        public Guid RefundId { get; set; }
    }
}
