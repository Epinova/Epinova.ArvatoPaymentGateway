namespace Epinova.ArvatoPaymentGateway
{
    internal class CustomerLookupResponseDto : ResponseDtoBase
    {
        public UserProfileDto[] UserProfiles { get; set; }
    }
}
