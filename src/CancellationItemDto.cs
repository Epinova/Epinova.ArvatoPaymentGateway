using Epinova.ArvatoPaymentGateway.Base;

namespace Epinova.ArvatoPaymentGateway
{
    internal class CancellationItemDto : BaseOrderItemDto
    {
        public string CancellationNumber { get; set; }
    }
}