using Epinova.ArvatoPaymentGateway.Base;

namespace Epinova.ArvatoPaymentGateway
{
    public class Capture : BaseOrder
    {
        public decimal TotalRefundedAmount { get; set; }
    }
}