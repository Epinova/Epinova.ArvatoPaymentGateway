namespace Epinova.ArvatoPaymentGateway
{
    public class CreditResponse
    {
        public string[] CreditNumbers { get; set; }
        public decimal TotalAuthorizedAmount { get; set; }
        public decimal TotalCapturedAmount { get; set; }
    }
}
