namespace Epinova.ArvatoPaymentGateway
{
    internal class PaymentDto
    {
        public AccountProductDto Account { get; set; }
        public InstallmentDto Installment { get; set; }
        public PaymentTypeDto Type { get; set; }
    }
}
