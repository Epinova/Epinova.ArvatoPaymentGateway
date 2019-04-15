namespace Epinova.ArvatoPaymentGateway
{
    internal enum ResponseMessageActionDto
    {
        Unavailable,
        AskConsumerToConfirm,
        AskConsumerToReEnterData,
        OfferSecurePaymentMethods,
        RequiresSsn
    }
}