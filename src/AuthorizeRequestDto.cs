namespace Epinova.ArvatoPaymentGateway
{
    internal class AuthorizeRequestDto
    {
        public CustomerRequestDto Customer { get; set; }
        public OrderInfoDto Order { get; set; }
        public PaymentDto Payment { get; set; }
    }
}