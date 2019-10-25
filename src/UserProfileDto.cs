namespace Epinova.ArvatoPaymentGateway
{
    internal class UserProfileDto
    {
        public AddressDto[] AddressList { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
    }
}
