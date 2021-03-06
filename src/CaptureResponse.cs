﻿namespace Epinova.ArvatoPaymentGateway
{
    public class CaptureResponse
    {
        public decimal AuthorizedAmount { get; set; }
        public decimal CapturedAmount { get; set; }
        public string CaptureNumber { get; set; }
        public decimal UnauthorizedAmount { get; set; }
    }
}
