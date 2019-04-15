namespace Epinova.ArvatoPaymentGateway
{
    internal class AuthorizeResponseDto : ResponseDtoBase
    {
        public string CheckoutId { get; set; }
        public CustomerResponseDto Customer { get; set; }
        public OutcomeTypeDto Outcome { get; set; }
        public string ReservationId { get; set; }
        public ResponseMessageDto[] RiskCheckMessages { get; set; }
    }
}