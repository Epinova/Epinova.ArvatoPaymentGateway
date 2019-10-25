using System;
using System.Threading.Tasks;

namespace Epinova.ArvatoPaymentGateway
{
    public interface IInvoiceGatewayService
    {
        /// <summary>
        /// Approves the payment for a specified customer and basket. Main use-cases are One-Step and Two-Step Authorization.
        /// Full fraud and credit scoring applied.
        /// </summary>
        Task<AuthorizeResponse> AuthorizeAsync(string authorizationKey, AuthorizeRequest request);

        /// <summary>
        /// Returns the available installment plans for the specific request amount.
        /// Returns monthly installment amount, interest and fees.
        /// </summary>
        Task<AvailableInstallmentPlansResponse> AvailableInstallmentPlansAsync(string authorizationKey, AvailableInstallmentPlansRequest request);

        /// <summary>
        /// Cancel (Void) an authorization that has not been captured.
        /// </summary>
        Task<CancelResponse> CancelAsync(string authorizationKey, string orderNumber);

        /// <summary>
        /// Completes the payment that has been authorized. Typically done when the order is shipped. Can be a full or partial capture of the order amount.
        /// </summary>
        Task<CaptureResponse> CaptureAsync(string authorizationKey, CaptureRequest request);

        /// <summary>
        /// Completes the payment that has been authorized in full. Typically done when the order is shipped.
        /// </summary>
        Task<CaptureResponse> CaptureFullAsync(string authorizationKey, string orderNumber);

        /// <summary>
        /// Refunds a partially or fully captured payment.
        /// </summary>
        Task<CreditResponse> CreditAsync(string authorizationKey, CreditRequest request);

        /// <summary>
        /// Refunds a captured payment in full.
        /// </summary>
        Task<CreditResponse> CreditFullAsync(string authorizationKey, string orderNumber);

        /// <summary>
        /// Returns the contents of the specified order 
        /// </summary>
        Task<OrderResponse> GetOrderAsync(string authorizationKey, string orderNumber);

        /// <summary>
        /// Get the version number of the API. Can be useful for integration testing.
        /// </summary>
        Task<Version> GetVersionAsync(string authorizationKey);

        /// <summary>
        /// Helper method to analyze known error codes.
        /// </summary>
        bool IsAfterPayOptionUnavailable(string errorCode);

        /// <summary>
        /// Check API presence. Useful for integration testing
        /// </summary>
        Task<bool> IsApiUpAsync(string authorizationKey);

        /// <summary>
        /// Helper method to analyze known error codes.
        /// </summary>
        bool IsMissingSsnError(string errorCode);

        /// <summary>
        /// Helper method to analyze known error codes.
        /// </summary>
        bool IsUserError(string errorCode);
    }
}
