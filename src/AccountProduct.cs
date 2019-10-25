namespace Epinova.ArvatoPaymentGateway
{
    public class AccountProduct : IIdempotent
    {
        public int ProfileNo { get; set; }

        public int GetIdempotentKey()
        {
            return ProfileNo;
        }
    }
}
