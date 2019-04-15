using Epinova.ArvatoPaymentGateway.Base;


namespace Epinova.ArvatoPaymentGateway
{
    public class AvailableInstallmentPlansResponse : BaseArvatoResponse
    {
        public InstallmentInfo[] AvailableInstallmentPlans { get; set; }
    }
}