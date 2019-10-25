using System;
using Epinova.ArvatoPaymentGateway.Base;

namespace Epinova.ArvatoPaymentGateway
{
    internal class CaptureDto : BaseOrderDto
    {
        public string AccountProfileNumber { get; set; }
        public CaptureItemDto[] CaptureItems { get; set; }
        public DateTime ContractDate { get; set; }
        public string DirectDebitBankAccount { get; set; }
        public string DirectDebitSwift { get; set; }
        public DateTime DueDate { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal InstallmentCustomerInterestRate { get; set; }
        public decimal InstallmentProfileNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int InvoiceProfileNumber { get; set; }
        public int NumberOfInstallments { get; set; }
        public string Ocr { get; set; }
        public DateTime OrderDate { get; set; }
        public string OurReference { get; set; }
        public ShippingDetailsWithNumberDto[] ShippingDetails { get; set; }
        public decimal TotalRefundedAmount { get; set; }
        public string YourReference { get; set; }
    }
}
