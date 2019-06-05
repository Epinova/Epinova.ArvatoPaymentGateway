namespace Epinova.ArvatoPaymentGateway
{
    public class Payment : IIdempotent
    {
        public AccountProduct Account { get; set; }
        public Installment Installment { get; set; }
        public PaymentType Type { get; set; }

        public int GetIdempotentKey()
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