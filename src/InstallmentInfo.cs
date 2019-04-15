namespace Epinova.ArvatoPaymentGateway
{
    public class InstallmentInfo
    {
        public decimal BasketAmount { get; set; }
        public decimal EffectiveAnnualPercentageRate { get; set; }
        public decimal EffectiveInterestRate { get; set; }
        public decimal FirstInstallmentAmount { get; set; }
        public decimal InstallmentAmount { get; set; }
        public int InstallmentProfileNumber { get; set; }
        public decimal InterestRate { get; set; }
        public decimal LastInstallmentAmount { get; set; }
        public decimal MonthlyFee { get; set; }
        public int NumberOfInstallments { get; set; }
        public string ReadMore { get; set; }
        public decimal StartupFee { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalInterestAmount { get; set; }
    }
}