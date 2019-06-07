using System.Threading.Tasks;

namespace Epinova.ArvatoPaymentGateway
{
    public interface ICustomerLookupService
    {
        /// <summary>
        /// Returns the customers address based on mobile number. Works only for private persons, cannot look up companies.
        /// </summary>
        Task<CustomerLookupResponse> LookupAsync(string authorizationKey, string phoneNumber);
    }
}