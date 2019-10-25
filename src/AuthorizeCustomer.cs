using System;
using Epinova.Infrastructure.Logging;

namespace Epinova.ArvatoPaymentGateway
{
    public class AuthorizeCustomer : IIdempotent, ICustomLogMessage
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

        public string ToLoggableString()
        {
            string identifier;

            if (String.IsNullOrWhiteSpace(Identifier))
                identifier = Identifier;
            else
                identifier = Identifier.Length > 6
                    ? Identifier.Substring(0, 6) + "XXXXX"
                    : Identifier;

            return $"Name '{FirstName} {LastName}', email: '{Email}', identifier: '{identifier}'";
        }

        public int GetIdempotentKey()
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
