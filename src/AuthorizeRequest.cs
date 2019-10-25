namespace Epinova.ArvatoPaymentGateway
{
    public class AuthorizeRequest : IIdempotent
    {
        public AuthorizeCustomer Customer { get; set; }
        public OrderInfo Order { get; set; }
        public Payment Payment { get; set; }

        public int GetIdempotentKey()
        {
            unchecked
            {
                int hashCode = Customer?.GetIdempotentKey() ?? 0;
                hashCode = (hashCode * 397) ^ (Order?.GetIdempotentKey() ?? 0);
                hashCode = (hashCode * 397) ^ (Payment?.GetIdempotentKey() ?? 0);
                return hashCode;
            }
        }
    }
}
