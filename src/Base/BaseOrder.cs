namespace Epinova.ArvatoPaymentGateway.Base
{
    public abstract class BaseOrder
    {
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}