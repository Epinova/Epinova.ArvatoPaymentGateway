namespace Epinova.ArvatoPaymentGateway
{
    internal class AvailableInstallmentPlansRequestDto
    {
        public decimal Amount { get; set; }
        public CountryCodeDto CountryCode { get; set; }
        public CurrencyDto Currency { get; set; }
    }
}