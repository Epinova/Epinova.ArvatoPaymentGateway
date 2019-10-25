namespace Epinova.ArvatoPaymentGateway
{
    public class ResponseMessage
    {
        public ResponseMessageAction ActionCode { get; set; }
        public string Code { get; set; }
        public string CustomerFacingMessage { get; set; }
        public string FieldReference { get; set; }
        public string Message { get; set; }
        public ResponseMessageType Type { get; set; }
    }
}
