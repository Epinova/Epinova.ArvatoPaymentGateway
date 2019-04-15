namespace Epinova.ArvatoPaymentGateway
{
    internal class CancellationDto
    {
        public decimal CancellationAmount { get; set; }
        public CancellationItemDto[] CancellationItems { get; set; }
        public string CancellationNo { get; set; }
        public string ParentTransactionReference { get; set; }
    }
}