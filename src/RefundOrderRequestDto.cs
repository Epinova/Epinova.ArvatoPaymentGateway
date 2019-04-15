namespace Epinova.ArvatoPaymentGateway
{
    internal class RefundOrderRequestDto
    {
        public string CaptureNumber { get; set; }
        public string CreditNoteNumber { get; set; }
        public RefundOrderItemDto[] OrderItems { get; set; }
        public string ParentTransactionReference { get; set; }
        public RefundTypeDto RefundType { get; set; }
    }
}