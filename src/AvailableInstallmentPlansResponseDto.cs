namespace Epinova.ArvatoPaymentGateway
{
    internal class AvailableInstallmentPlansResponseDto : ResponseDtoBase
    {
        public InstallmentInfoDto[] AvailableInstallmentPlans { get; set; }
    }
}