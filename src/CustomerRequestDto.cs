namespace Epinova.ArvatoPaymentGateway
{
    internal class CustomerRequestDto : CustomerDtoBase
    {
        public AddressDto Address { get; set; }
        public ConversationLanguageDto ConversationLanguage { get; set; }
        public CustomerCategoryDto CustomerCategory { get; set; }
        public string Email { get; set; }
        public string IdentificationNumber { get; set; }
        public string MobilePhone { get; set; }
        public string Phone { get; set; }
    }
}
