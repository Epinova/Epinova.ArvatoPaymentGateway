namespace Epinova.ArvatoPaymentGateway
{
    public class AuthorizeRequest
    {
        public AuthorizeCustomer Customer { get; set; }
        public OrderInfo Order { get; set; }
        public Payment Payment { get; set; }
    }
}