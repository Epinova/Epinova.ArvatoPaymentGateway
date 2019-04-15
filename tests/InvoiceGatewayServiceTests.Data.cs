using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using EPiServer.Logging;
using Moq;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public partial class InvoiceGatewayServiceTests
    {

        private string GetOrderData()
        {
            return @"
{
  ""orderDetails"": {
    ""orderId"": ""68467293-bb80-44f0-b4e4-2bba74baf092"",
    ""orderNumber"": ""ORDER00001"",
    ""totalNetAmount"": 550.0,
    ""totalGrossAmount"": 720.0,
    ""currency"": ""NOK"",
    ""orderChannelType"": ""CallCenter"",
    ""orderDeliveryType"": ""Express"",
    ""hasSeparateDeliveryAddress"": true,
    ""customer"": {
      ""customerNumber"": ""CUSTOMER1000"",
      ""identificationNumber"": ""SSN198411090111"",
      ""salutation"": ""Mr"",
      ""firstName"": ""Santa"",
      ""lastName"": ""Claus"",
      ""email"": ""santa.claus@northpole.com"",
      ""phone"": ""+3725551234"",
      ""mobilePhone"": ""06669874"",
      ""birthDate"": ""1984-12-08T00:00:00Z"",
      ""customerCategory"": ""Person"",
      ""address"": {
        ""street"": ""Ice road"",
        ""streetNumber"": ""55"",
        ""streetNumberAdditional"": ""a"",
        ""postalCode"": ""10088"",
        ""postalPlace"": ""North pole"",
        ""countryCode"": ""NO""
      }
    },
    ""insertedAt"": ""2016-01-29T12:39:28Z"",
    ""updatedAt"": ""2016-01-29T12:39:28Z""
  },
  ""captures"": [
    {
      ""captureId"": ""1c2575f8-d130-43e1-a1b3-43d487b7d6e3"",
      ""reservationId"": ""7b0c214a-878d-4a72-8c70-73d6ae204dce"",
      ""customerNumber"": ""CUSTOMER1000"",
      ""captureNumber"": ""SHIPMENT001"",
      ""orderNumber"": ""ORDER00001"",
      ""amount"": 720.0,
      ""totalRefundedAmount"": 720.0,
      ""currency"": ""NOK"",
      ""insertedAt"": ""2016-01-29T13:10:06Z"",
      ""updatedAt"": ""2016-01-29T13:41:05Z"",
      ""contractDate"": ""2016-01-29T11:10:06Z"",
      ""orderDate"": ""2016-01-29T10:42:34Z"",
      ""dueDate"": ""2016-01-29T11:10:06Z"",
      ""yourReference"": ""YOUR001"",
      ""ourReference"": ""OUR001"",
      ""invoiceProfileNumber"": 40001000,
      ""ocr"": ""OCR15568"",
      ""captureItems"": [
        {
          ""captureId"": ""1c2575f8-d130-43e1-a1b3-43d487b7d6e3"",
          ""productId"": ""ITEM1111"",
          ""groupId"": ""GROUP14"",
          ""description"": ""Presents"",
          ""netUnitPrice"": 100.0,
          ""grossUnitPrice"": 132.5,
          ""quantity"": 5.0,
          ""vatCategory"": ""HighCategory"",
          ""vatPercent"": 32.5,
          ""vatAmount"": 32.5,
          ""lineNumber"": 1
        },
        {
          ""captureId"": ""1c2575f8-d130-43e1-a1b3-43d487b7d6e3"",
          ""productId"": ""ITEM98556"",
          ""groupId"": ""GROUP02"",
          ""description"": ""Bag"",
          ""netUnitPrice"": 50.0,
          ""grossUnitPrice"": 57.5,
          ""quantity"": 1.0,
          ""vatCategory"": ""MiddleCategory"",
          ""vatPercent"": 15.0,
          ""vatAmount"": 7.5,
          ""lineNumber"": 2
        }
      ],
      ""shippingDetails"": [
        {
          ""shippingNumber"": 1,
          ""type"": ""Shipment"",
          ""shippingCompany"": ""UPS"",
          ""trackingId"": ""1Z9999999999999999"",
          ""trackingUrl"": ""https://wwwapps.ups.com/tracking/tracking.cgi?tracknum=1Z9999999999999999""
        }
      ]
    }
  ],
  ""refunds"": [
    {
      ""refundId"": ""82bb036f-ac1f-4394-9cd1-6c8111a8199c"",
      ""reservationId"": ""7b0c214a-878d-4a72-8c70-73d6ae204dce"",
      ""customerNumber"": ""CUSTOMER1000"",
      ""refundNumber"": ""SHIPMENT001R00"",
      ""orderNumber"": ""ORDER00001"",
      ""amount"": -720.0,
      ""balance"": -720.0,
      ""currency"": ""NOK"",
      ""insertedAt"": ""2016-01-29T13:41:05Z"",
      ""captureNumber"": ""SHIPMENT001"",
      ""refundItems"": [
        {
          ""refundId"": ""82bb036f-ac1f-4394-9cd1-6c8111a8199c"",
          ""productId"": ""ITEM1111"",
          ""groupId"": ""GROUP14"",
          ""description"": ""Presents"",
          ""netUnitPrice"": -100.0,
          ""grossUnitPrice"": -132.5,
          ""quantity"": 5.0,
          ""vatCategory"": ""HighCategory"",
          ""vatPercent"": 32.5,
          ""vatAmount"": -32.5
        },
        {
          ""refundId"": ""82bb036f-ac1f-4394-9cd1-6c8111a8199c"",
          ""productId"": ""ITEM98556"",
          ""groupId"": ""GROUP02"",
          ""description"": ""Bag"",
          ""netUnitPrice"": -50.0,
          ""grossUnitPrice"": -57.5,
          ""quantity"": 1.0,
          ""vatCategory"": ""MiddleCategory"",
          ""vatPercent"": 15.0,
          ""vatAmount"": -7.5,
          ""lineNumber"": 2
        }
      ]
    }
  ],
  ""cancellations"": [
    {
      ""cancellationNo"": ""ORDER00001C00"",
      ""cancellationAmount"": 40.0,
      ""cancellationItems"": [
        {
          ""productId"": ""ITEM0001"",
          ""groupId"": ""GROUP01"",
          ""description"": ""Presents"",
          ""netUnitPrice"": 20.0,
          ""grossUnitPrice"": 20.0,
          ""quantity"": 2.0
        }
      ]
    },
    {
      ""cancellationNo"": ""ORDER00001C01"",
      ""cancellationAmount"": 690.0
    }
  ]
}
";
        }
    }
}