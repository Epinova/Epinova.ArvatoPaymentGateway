namespace Epinova.ArvatoPaymentGateway
{
    internal class OrderResponseDto : ResponseDtoBase
    {
        public CancellationDto[] Cancellations { get; set; }
        public CaptureDto[] Captures { get; set; }
        public ResponseOrderDto OrderDetails { get; set; }
        public RefundDto[] Refunds { get; set; }
    }
}
