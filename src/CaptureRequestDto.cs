namespace Epinova.ArvatoPaymentGateway
{
    internal class CaptureRequestDto
    {
        public int CampaignNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public OrderSummaryDto OrderDetails { get; set; }
        public ShippingDetailsDto[] ShippingDetails { get; set; }
    }
}