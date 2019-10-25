using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Epinova.ArvatoPaymentGateway;
using EPiServer.Logging;
using Moq;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public partial class InvoiceGatewayServiceTests
    {
        private readonly Mock<ILogger> _logMock;
        private readonly TestableHttpMessageHandler _messageHandler;
        private readonly InvoiceGatewayService _service;

        public InvoiceGatewayServiceTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new InvoiceGatewayMappingProfile()); });
            _messageHandler = new TestableHttpMessageHandler();
            _logMock = new Mock<ILogger>();
            InvoiceGatewayService.Client = new HttpClient(_messageHandler) { BaseAddress = new Uri("https://fake.api.uri/") };
            _service = new InvoiceGatewayService(_logMock.Object, mapperConfiguration.CreateMapper());
        }


        [Fact]
        public async Task Authorize_ServiceFails_ReturnsNull()
        {
            _messageHandler.SendAsyncThrows(new Exception());
            AuthorizeResponse result = await _service.AuthorizeAsync(Factory.GetString(), new AuthorizeRequest());

            Assert.Null(result);
        }

        [Fact]
        public async Task Authorize_ServiceReturnsNull_ReturnsNull()
        {
            _messageHandler.SendAsyncReturns(null);
            AuthorizeResponse result = await _service.AuthorizeAsync(Factory.GetString(), new AuthorizeRequest());

            Assert.Null(result);
        }

        [Fact]
        public async Task Authorize_ServiceReturnsUnauthorizedStatus_ReturnsError()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            AuthorizeResponse result = await _service.AuthorizeAsync(Factory.GetString(), new AuthorizeRequest());

            Assert.True(result.HasError);
        }

        [Fact]
        public async Task Authorize_ServiceReturnsValidJson_ReturnsCorrectAuthStatus()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"Outcome\": \"Accepted\", \"ReservationId\": \"123456\", \"Customer\": {\"CustomerNumber\": \"789\"}}")
            });
            AuthorizeResponse result = await _service.AuthorizeAsync(Factory.GetString(), new AuthorizeRequest());

            Assert.True(result.IsAuthorized);
        }

        [Fact]
        public async Task Authorize_ServiceReturnsValidJson_ReturnsCorrectCustomerNumber()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"Outcome\": \"Accepted\", \"ReservationId\": \"123456\", \"Customer\": {\"CustomerNumber\": \"789\"}}")
            });
            AuthorizeResponse result = await _service.AuthorizeAsync(Factory.GetString(), new AuthorizeRequest());

            Assert.Equal("789", result.CustomerNumber);
        }

        [Fact]
        public async Task Authorize_ServiceReturnsValidJson_ReturnsCorrectReservationId()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"Outcome\": \"Accepted\", \"ReservationId\": \"123456\", \"Customer\": {\"CustomerNumber\": \"789\"}}")
            });
            AuthorizeResponse result = await _service.AuthorizeAsync(Factory.GetString(), new AuthorizeRequest());

            Assert.Equal("123456", result.ReservationId);
        }

        [Fact]
        public async Task Cancel_ServiceReturnsValidJson_ReturnsResponseInstance()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"totalCapturedAmount\": 0.0,\"totalAuthorizedAmount\": 248.0}")
            });
            CancelResponse result = await _service.CancelAsync(Factory.GetString(), Factory.GetString());

            Assert.IsType<CancelResponse>(result);
        }

        [Fact]
        public async Task CaptureFull_ServiceReturnsValidJson_ReturnsResponseInstance()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"capturedAmount\": 248.0000,\"authorizedAmount\": 248.0000,\"remainingAuthorizedAmount\": 0.0,\"captureNumber\": \"800007901\"}")
            });
            CaptureResponse result = await _service.CaptureFullAsync(Factory.GetString(), Factory.GetString());

            Assert.IsType<CaptureResponse>(result);
        }

        [Fact]
        public async Task CreditFull_ServiceReturnsValidJson_ReturnsResponseInstance()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"totalCapturedAmount\": 248.0000,\"totalAuthorizedAmount\": 248.0000,\"refundNumbers\": [\"800007902\"]}")
            });
            CreditResponse result = await _service.CreditFullAsync(Factory.GetString(), Factory.GetString());

            Assert.IsType<CreditResponse>(result);
        }

        [Fact]
        public async Task GetOrder_ServiceFails_ReturnsNull()
        {
            _messageHandler.SendAsyncThrows(new Exception());
            OrderResponse result = await _service.GetOrderAsync(Factory.GetString(), Factory.GetString());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetOrder_ServiceReturnsNull_ReturnsNull()
        {
            _messageHandler.SendAsyncReturns(null);
            OrderResponse result = await _service.GetOrderAsync(Factory.GetString(), Factory.GetString());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetOrder_ServiceReturnsUnauthorizedStatus_ReturnsNull()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            OrderResponse result = await _service.GetOrderAsync(Factory.GetString(), Factory.GetString());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetOrder_ServiceReturnsValidJson_ReturnsCorrectOrderNumber()
        {
            string jsonContent = GetOrderData();

            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonContent)
            });
            OrderResponse result = await _service.GetOrderAsync(Factory.GetString(), Factory.GetString());

            Assert.Equal("ORDER00001", result.OrderDetails.OrderNumber);
        }

        [Fact]
        public async Task GetVersion_ParseResultFails_ReturnsNull()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{ 'Some': 'random', 'unparasable': 'json' }")
            });
            Version result = await _service.GetVersionAsync(Factory.GetString());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetVersion_ServiceReturnsNull_LogsError()
        {
            _messageHandler.SendAsyncReturns(null);
            await _service.GetVersionAsync(Factory.GetString());

            _logMock.VerifyLog(Level.Error, "Version query failed. Service response was NULL", Times.Once());
        }

        [Fact]
        public async Task GetVersion_ServiceReturnsNull_ReturnsNull()
        {
            _messageHandler.SendAsyncReturns(null);
            Version result = await _service.GetVersionAsync(Factory.GetString());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetVersion_ServiceReturnsUnauthorizedStatus_ReturnsNull()
        {
            string transactionId = Factory.GetString();
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            Version result = await _service.GetVersionAsync(transactionId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetVersion_ServiceReturnsValidJson_ReturnsCorrectVersionNumber()
        {
            const string versionString = "3.0.6400.31841";
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($"{{ 'version': '{versionString}' }}")
            });
            Version result = await _service.GetVersionAsync(Factory.GetString());

            Assert.Equal(new Version(versionString), result);
        }

        [Fact]
        public async Task IsApiUp_ServiceReturnsHttpStatusOK_ReturnsTrue()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{ }")
            });
            bool result = await _service.IsApiUpAsync(Factory.GetString());

            Assert.True(result);
        }

        [Fact]
        public async Task IsApiUp_ServiceReturnsNull_LogsError()
        {
            _messageHandler.SendAsyncReturns(null);
            await _service.IsApiUpAsync(Factory.GetString());

            _logMock.VerifyLog(Level.Error, "Status query failed. Service response was NULL", Times.Once());
        }

        [Fact]
        public async Task IsApiUp_ServiceReturnsNull_ReturnsFalse()
        {
            _messageHandler.SendAsyncReturns(null);
            bool result = await _service.IsApiUpAsync(Factory.GetString());

            Assert.False(result);
        }

        [Fact]
        public async Task IsApiUp_ServiceReturnsUnauthorizedStatus_ReturnsFalse()
        {
            string transactionId = Factory.GetString();
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            bool result = await _service.IsApiUpAsync(transactionId);

            Assert.False(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Lookup_PhoneNumberIsNullOrEmptyOrWhite_DoesNotCallApi(string phoneNumber)
        {
            CustomerLookupResponse result = await _service.LookupAsync(Factory.GetString(), phoneNumber);

            Assert.Equal(0, _messageHandler.CallCount());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Lookup_PhoneNumberIsNullOrEmptyOrWhite_ReturnsNull(string phoneNumber)
        {
            CustomerLookupResponse result = await _service.LookupAsync(Factory.GetString(), phoneNumber);

            Assert.Null(result);
        }

        [Fact]
        public async Task Lookup_ServiceReturns404_LogWarning()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("[ {\"Code\": \"Some error code\"} ]")
            });
            await _service.LookupAsync(Factory.GetString(), Factory.GetString());

            _logMock.VerifyLog<string>(Level.Warning, Times.Once());
        }

        [Fact]
        public async Task Lookup_ServiceReturns404_ReturnsNull()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("[ {\"Code\": \"Some error code\"} ]")
            });
            CustomerLookupResponse result = await _service.LookupAsync(Factory.GetString(), Factory.GetString());

            Assert.Null(result);
        }

        [Fact]
        public async Task Lookup_ServiceReturnsNothing_LogError()
        {
            _messageHandler.SendAsyncReturns(null);
            await _service.LookupAsync(Factory.GetString(), Factory.GetString());

            _logMock.VerifyLog<string>(Level.Error, Times.Once());
        }

        [Fact]
        public async Task Lookup_ServiceReturnsNothing_ReturnsNull()
        {
            _messageHandler.SendAsyncReturns(null);
            CustomerLookupResponse result = await _service.LookupAsync(Factory.GetString(), Factory.GetString());

            Assert.Null(result);
        }

        [Fact]
        public async Task Lookup_ServiceReturnsValidJson_ReturnsResponseInstance()
        {
            _messageHandler.SendAsyncReturns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    "{ \"userProfiles\": [ { \"firstName\": \"koko\", \"lastName\": \"themonkey\", \"addressList\": [ { \"street\": \"Arbeidersamfunnets Plass 1\", \"city\": \"Oslo\", \"postalCode\": \"0181\", \"country\": \"Norway\", \"countryCode\": \"NO\"\r\n } ] } ] } ")
            });
            CustomerLookupResponse result = await _service.LookupAsync(Factory.GetString(), Factory.GetString());

            Assert.IsType<CustomerLookupResponse>(result);
        }
    }
}
