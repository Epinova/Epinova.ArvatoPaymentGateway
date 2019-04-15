using System.Collections.Generic;

namespace Epinova.ArvatoPaymentGateway
{
    public class CreditRequest
    {
        public CreditRequest()
        {
            OrderItems = new List<CreditOrderItem>();
        }

        public string CaptureNumber { get; set; }
        public string CreditNoteNumber { get; set; }
        public List<CreditOrderItem> OrderItems { get; set; }
        public string OrderNumber { get; set; }
    }
}