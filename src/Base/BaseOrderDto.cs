using System;

namespace Epinova.ArvatoPaymentGateway.Base
{
    internal abstract class BaseOrderDto
    {
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string CaptureNumber { get; set; }
        public CurrencyDto Currency { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime InsertedAt { get; set; }
        public string OrderNumber { get; set; }
        public string ParentTransactionReference { get; set; }
        public Guid ReservationId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}