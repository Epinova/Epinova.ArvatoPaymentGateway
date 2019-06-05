namespace Epinova.ArvatoPaymentGateway
{
    public class AuthorizeCustomer
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Identifier { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string PostalPlace { get; set; }

        public override int GetHashCode()
        {
            return CalculateHash();
        }

        private int CalculateHash()
        {
            unchecked
            {
                int hashCode = Address?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (City?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Email?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (FirstName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Identifier?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (LastName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Phone?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (PostalCode?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (PostalPlace?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}