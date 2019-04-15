using System.Threading.Tasks;

namespace Epinova.ArvatoPaymentGateway
{
    public interface ICustomerLookupService
    {
        Task<CustomerLookupResponse> LookupAsync(string authorizationKey, string phoneNumber);
    }
}