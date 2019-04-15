namespace Epinova.ArvatoPaymentGateway
{
    internal class CustomerResponseDto : CustomerDtoBase
    {
        public AddressDto[] AddressList { get; set; }
    }
}