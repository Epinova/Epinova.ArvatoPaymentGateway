namespace Epinova.ArvatoPaymentGateway
{
    internal class CancelResponseDto : ResponseDtoBase
    {
        public decimal TotalAuthorizedAmount { get; set; }
        public decimal TotalCapturedAmount { get; set; }
    }
}