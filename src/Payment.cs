namespace Epinova.ArvatoPaymentGateway
{
    public class Payment
    {
        public AccountProduct Account { get; set; }
        public Installment Installment { get; set; }
        public PaymentType Type { get; set; }
    }
}