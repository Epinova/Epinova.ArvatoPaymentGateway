namespace Epinova.ArvatoPaymentGateway
{
    internal class RefundOrderResponseDto : ResponseDtoBase
    {
        public string[] RefundNumbers { get; set; }
        public decimal TotalAuthorizedAmount { get; set; }
        public decimal TotalCapturedAmount { get; set; }
    }
}
