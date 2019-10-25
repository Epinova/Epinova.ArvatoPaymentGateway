namespace Epinova.ArvatoPaymentGateway.Base
{
    public abstract class BaseArvatoResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
    }
}
