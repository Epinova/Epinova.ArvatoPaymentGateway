namespace Epinova.ArvatoPaymentGateway
{
    public class Payment
    {
        public AccountProduct Account { get; set; }
        public Installment Installment { get; set; }
        public PaymentType Type { get; set; }

        public override int GetHashCode()
        {
            return CalculateHash();
        }

        private int CalculateHash()
        {
            unchecked
            {
                int hashCode = Account?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Installment?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (int) Type;
                return hashCode;
            }
        }
    }
}