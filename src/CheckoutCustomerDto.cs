using System;

namespace Epinova.ArvatoPaymentGateway
{
    internal class CheckoutCustomerDto : CustomerRequestDto
    {
        public DateTime BirthDate { get; set; }
        public CustomerRiskDto RiskData { get; set; }
    }
}