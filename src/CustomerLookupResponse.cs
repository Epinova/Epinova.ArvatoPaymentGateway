namespace Epinova.ArvatoPaymentGateway
{
    public class CustomerLookupResponse
    {
        public readonly string PhonePrefix = "+47";
        public string City { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
    }
}