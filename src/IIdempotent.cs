namespace Epinova.ArvatoPaymentGateway
{
    public interface IIdempotent
    {
        int GetIdempotentKey();
    }
}