namespace Epinova.ArvatoPaymentGateway
{
    public class AuthorizeRequest
    {
        public AuthorizeCustomer Customer { get; set; }
        public OrderInfo Order { get; set; }
        public Payment Payment { get; set; }

        public override int GetHashCode()
        {
            return CalculateHash();
        }

        private int CalculateHash()
        {
            unchecked
            {
                int hashCode = Customer?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Order?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Payment?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}