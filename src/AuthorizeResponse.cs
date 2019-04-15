using Epinova.ArvatoPaymentGateway.Base;

namespace Epinova.ArvatoPaymentGateway
{
    public class AuthorizeResponse : BaseArvatoResponse
    {
        public Address[] AddressList { get; set; }
        public string CheckoutId { get; set; }
        public string CustomerNumber { get; set; }
        public bool IsAuthorized { get; set; }
        public string ReservationId { get; set; }
        public ResponseMessage[] RiskCheckMessages { get; set; }
    }
}