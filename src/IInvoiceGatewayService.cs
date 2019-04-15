using System;
using System.Threading.Tasks;

namespace Epinova.ArvatoPaymentGateway
{
    public interface IInvoiceGatewayService
    {
        Task<AuthorizeResponse> AuthorizeAsync(string authorizationKey, AuthorizeRequest request);
        Task<AvailableInstallmentPlansResponse> AvailableInstallmentPlansAsync(string authorizationKey, AvailableInstallmentPlansRequest request);
        Task<CancelResponse> CancelAsync(string authorizationKey, string orderNumber);
        Task<CaptureResponse> CaptureAsync(string authorizationKey, CaptureRequest request);
        Task<CaptureResponse> CaptureFullAsync(string authorizationKey, string orderNumber);
        Task<CreditResponse> CreditAsync(string authorizationKey, CreditRequest request);
        Task<CreditResponse> CreditFullAsync(string authorizationKey, string orderNumber);
        Task<OrderResponse> GetOrderAsync(string authorizationKey, string orderNumber);
        Task<Version> GetVersionAsync(string authorizationKey);
        bool IsAfterPayOptionUnavailable(string errorCode);
        Task<bool> IsApiUpAsync(string authorizationKey);
        bool IsMissingSsnError(string errorCode);
        bool IsUserError(string errorCode);
    }
}