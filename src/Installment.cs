namespace Epinova.ArvatoPaymentGateway
{
    public class Installment
    {
        public int ProfileNo { get; set; }

        public override int GetHashCode()
        {
            return CalculateHash();
        }

        private int CalculateHash()
        {
            return ProfileNo;
        }
    }
}