namespace Epinova.ArvatoPaymentGateway
{
    internal class ResponseMessageDto : ResponseDtoBase
    {
        public ResponseMessageActionDto ActionCode { get; set; }
        public string Code { get; set; }
        public string CustomerFacingMessage { get; set; }
        public string FieldReference { get; set; }
        public string Message { get; set; }
        public ResponseMessageTypeDto Type { get; set; }
    }
}