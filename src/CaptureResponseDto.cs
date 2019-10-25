namespace Epinova.ArvatoPaymentGateway
{
    internal class CaptureResponseDto : ResponseDtoBase
    {
        public decimal AuthorizedAmount { get; set; }
        public decimal CapturedAmount { get; set; }
        public string CaptureNumber { get; set; }
        public decimal RemainingAuthorizedAmount { get; set; }
    }
}
