namespace Epinova.ArvatoPaymentGateway
{
    public class Installment : IIdempotent
    {
        public int ProfileNo { get; set; }

        public int GetIdempotentKey()
        {
            return ProfileNo;
        }
    }
}