using System;
using Epinova.ArvatoPaymentGateway.Base;

namespace Epinova.ArvatoPaymentGateway
{
    internal class CaptureItemDto : BaseOrderItemDto
    {
        public Guid CaptureId { get; set; }
    }
}