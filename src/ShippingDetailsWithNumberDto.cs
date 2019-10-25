namespace Epinova.ArvatoPaymentGateway
{
    internal class ShippingDetailsWithNumberDto : ShippingDetailsDto
    {
        public int ShippingNumber { get; set; }

        public string TrackingUrl { get; set; }
    }
}
