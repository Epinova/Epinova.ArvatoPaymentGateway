using System;

namespace Epinova.ArvatoPaymentGateway
{
    internal class CustomerRiskDto
    {
        public string AcquisitionChannel { get; set; }
        public string CustomerCardClassification { get; set; }
        public DateTime CustomerCardSince { get; set; }
        public string CustomerClassification { get; set; }
        public decimal CustomerIndividualScore { get; set; }
        public DateTime CustomerSince { get; set; }
        public bool ExistingCustomer { get; set; }
        public bool HasCustomerCard { get; set; }
        public string IpAddress { get; set; }
        public bool MarketingOptIn { get; set; }
        public int NumberOfTransactions { get; set; }
        public string ProfileTrackingId { get; set; }
        public bool VerifiedCustomerIdentification { get; set; }
    }
}
