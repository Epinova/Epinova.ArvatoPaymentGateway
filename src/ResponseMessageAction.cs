namespace Epinova.ArvatoPaymentGateway
{
    public enum ResponseMessageAction
    {
        Unavailable,
        AskConsumerToConfirm,
        AskConsumerToReEnterData,
        OfferSecurePaymentMethods,
        RequiresSsn
    }
}
