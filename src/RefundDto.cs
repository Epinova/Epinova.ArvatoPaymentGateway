using System;
using Epinova.ArvatoPaymentGateway.Base;

namespace Epinova.ArvatoPaymentGateway
{
    internal class RefundDto : BaseOrderDto
    {
        public Guid RefundId { get; set; }
        public RefundItemDto[] RefundItems { get; set; }
        public string RefundNumber { get; set; }
    }
}