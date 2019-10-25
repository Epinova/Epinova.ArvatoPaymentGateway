using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Epinova.Infrastructure;
using Epinova.Infrastructure.Logging;
using EPiServer.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Epinova.ArvatoPaymentGateway
{
    public class InvoiceGatewayService : RestServiceBase, IInvoiceGatewayService, ICustomerLookupService
    {
        internal static HttpClient Client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["AfterPay.Api.BaseAddress"] ?? "https://api.afterpay.io/") };
        private readonly ILogger _log;
        private readonly IMapper _mapper;


        public InvoiceGatewayService(ILogger log, IMapper mapper) : base(log)
        {
            _log = log;
            _mapper = mapper;
        }


        public async Task<CustomerLookupResponse> LookupAsync(string authorizationKey, string phoneNumber)
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
                return null;

            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, "api/v3/lookup/customer", HttpMethod.Post);
            var requestDto = new { MobilePhone = phoneNumber };

            string content = await SerializeDto(requestDto);

            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage), true);

            if (responseMessage == null)
            {
                _log.Error(new { message = "Lookup query failed. Service response was NULL or not OK", phoneNumber });
                return null;
            }

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                ResponseMessageDto[] errorList = await ParseJsonArrayAsync<ResponseMessageDto>(responseMessage);
                _log.Warning(new { message = "Lookup query faild. ", phoneNumber, responseMessage.StatusCode, errorList });
                return null;
            }

            CustomerLookupResponseDto responseDto = await ParseJsonAsync<CustomerLookupResponseDto>(responseMessage);

            if (responseDto.HasError)
            {
                _log.Error(new { message = "Lookup query failed", phoneNumber, responseDto.ErrorMessage });
                return null;
            }

            var result = _mapper.Map<CustomerLookupResponse>(responseDto.UserProfiles.FirstOrDefault());
            if (string.IsNullOrEmpty(result.MobileNumber))
                result.MobileNumber = phoneNumber;

            _log.Debug(new { message = "Lookup query succeeded", phoneNumber, result });

            return result;
        }

        public async Task<AuthorizeResponse> AuthorizeAsync(string authorizationKey, AuthorizeRequest request)
        {
            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, "api/v3/checkout/authorize", HttpMethod.Post, request.GetIdempotentKey());
            var requestDto = _mapper.Map<AuthorizeRequestDto>(request);

            string content = await SerializeDto(requestDto);

            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage), true);

            if (responseMessage == null)
            {
                _log.Error(new { message = "Authorize query failed. Service response was NULL or not OK", request });
                return null;
            }

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                ResponseMessageDto[] errorList = await ParseJsonArrayAsync<ResponseMessageDto>(responseMessage);
                _log.Warning(new { message = "Authorize query faild. ", request, responseMessage.StatusCode, errorList });

                string errorMessage = GetErrorMessage(errorList, out string errorCode);
                return new AuthorizeResponse
                {
                    HasError = true,
                    ErrorCode = errorCode,
                    ErrorMessage = errorMessage
                };
            }

            AuthorizeResponseDto responseDto = await ParseJsonAsync<AuthorizeResponseDto>(responseMessage);

            if (responseDto.HasError)
            {
                _log.Error(new { message = "Authorize query failed", request, responseDto.ErrorMessage });
                return null;
            }

            var result = _mapper.Map<AuthorizeResponse>(responseDto);

            _log.Information(new { message = "Authorize query succeeded", orderNumber = request.Order?.Number, result });

            return result;
        }

        public async Task<AvailableInstallmentPlansResponse> AvailableInstallmentPlansAsync(string authorizationKey, AvailableInstallmentPlansRequest request)
        {
            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, "api/v3/lookup/installment-plans", HttpMethod.Post);
            var requestDto = _mapper.Map<AvailableInstallmentPlansRequestDto>(request);

            string content = await SerializeDto(requestDto);

            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage), true);

            if (responseMessage == null)
            {
                _log.Error(new { message = "AvailableInstallmentPlans query failed. Service response was NULL or not OK", request });
                return null;
            }

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                ResponseMessageDto[] errorList = await ParseJsonArrayAsync<ResponseMessageDto>(responseMessage);
                _log.Warning(new { message = "AvailableInstallmentPlans query faild. ", request, responseMessage.StatusCode, errorList });
                ResponseMessageDto firstError = errorList.FirstOrDefault();
                return new AvailableInstallmentPlansResponse
                {
                    HasError = true,
                    ErrorCode = firstError?.Code,
                    ErrorMessage = firstError?.CustomerFacingMessage
                };
            }

            AvailableInstallmentPlansResponseDto responseDto = await ParseJsonAsync<AvailableInstallmentPlansResponseDto>(responseMessage);

            if (responseDto.HasError)
            {
                _log.Error(new { message = "AvailableInstallmentPlans query failed", request, responseDto.ErrorMessage });
                return new AvailableInstallmentPlansResponse
                {
                    HasError = true,
                    ErrorMessage = responseDto.ErrorMessage
                };
            }

            var result = _mapper.Map<AvailableInstallmentPlansResponse>(responseDto);

            _log.Debug(new { message = "AvailableInstallmentPlans query succeeded", request.Amount, result });

            return result;
        }

        public async Task<CancelResponse> CancelAsync(string authorizationKey, string orderNumber)
        {
            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, $"api/v3/orders/{orderNumber}/voids", HttpMethod.Post, orderNumber);

            //INFO: No POST data needed for voids. --tarjei

            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage), true);

            if (responseMessage == null)
            {
                _log.Error(new { message = "Cancel query failed. Service response was NULL or not OK", orderNumber });
                return null;
            }

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                ResponseMessageDto[] errorList = await ParseJsonArrayAsync<ResponseMessageDto>(responseMessage);
                _log.Warning(new { message = "Cancel query faild.", orderNumber, responseMessage.StatusCode, errorList });
                return null;
            }

            CancelResponseDto responseDto = await ParseJsonAsync<CancelResponseDto>(responseMessage);

            if (responseDto.HasError)
            {
                _log.Error(new { message = "Cancel query failed", orderNumber, responseDto.ErrorMessage });
                return null;
            }

            var result = _mapper.Map<CancelResponse>(responseDto);

            _log.Information(new { message = "Cancel query succeeded", orderNumber, result });

            return result;
        }

        public async Task<CaptureResponse> CaptureAsync(string authorizationKey, CaptureRequest request)
        {
            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, $"api/v3/orders/{request.OrderNumber}/captures", HttpMethod.Post, request.GetIdempotentKey());
            var requestDto = _mapper.Map<CaptureRequestDto>(request);

            string content = await SerializeDto(requestDto);

            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage), true);

            if (responseMessage == null)
            {
                _log.Error(new { message = "Capture query failed. Service response was NULL or not OK", request.OrderNumber });
                return null;
            }

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                ResponseMessageDto[] errorList = await ParseJsonArrayAsync<ResponseMessageDto>(responseMessage);
                _log.Warning(new { message = "Capture query faild.", request.OrderNumber, responseMessage.StatusCode, errorList });
                return null;
            }

            CaptureResponseDto responseDto = await ParseJsonAsync<CaptureResponseDto>(responseMessage);

            if (responseDto.HasError)
            {
                _log.Error(new { message = "Capture query failed", request.OrderNumber, responseDto.ErrorMessage });
                return null;
            }

            var result = _mapper.Map<CaptureResponse>(responseDto);

            _log.Information(new { message = "Capture query succeeded", request.OrderNumber, result });

            return result;
        }

        public async Task<CaptureResponse> CaptureFullAsync(string authorizationKey, string orderNumber)
        {
            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, $"api/v3/orders/{orderNumber}/captures", HttpMethod.Post, orderNumber);

            //INFO: No POST data needed for full captures. --tarjei

            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage), true);

            if (responseMessage == null)
            {
                _log.Error(new { message = "Full capture query failed. Service response was NULL or not OK", orderNumber });
                return null;
            }

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                ResponseMessageDto[] errorList = await ParseJsonArrayAsync<ResponseMessageDto>(responseMessage);
                _log.Warning(new { message = "Full capture query faild.", orderNumber, responseMessage.StatusCode, errorList });
                return null;
            }

            CaptureResponseDto responseDto = await ParseJsonAsync<CaptureResponseDto>(responseMessage);

            if (responseDto.HasError)
            {
                _log.Error(new { message = "Full capture query failed", orderNumber, responseDto.ErrorMessage });
                return null;
            }

            var result = _mapper.Map<CaptureResponse>(responseDto);

            _log.Information(new { message = "Full capture query succeeded", orderNumber, result });

            return result;
        }

        public async Task<CreditResponse> CreditAsync(string authorizationKey, CreditRequest request)
        {
            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, $"api/v3/orders/{request.OrderNumber}/refunds", HttpMethod.Post, request.GetIdempotentKey());

            var requestDto = _mapper.Map<RefundOrderRequestDto>(request);

            string content = await SerializeDto(requestDto);

            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage), true);

            if (responseMessage == null)
            {
                _log.Error(new { message = "Credit query failed. Service response was NULL or not OK", request });
                return null;
            }

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                ResponseMessageDto[] errorList = await ParseJsonArrayAsync<ResponseMessageDto>(responseMessage);
                _log.Warning(new { message = "Credit query faild.", request, responseMessage.StatusCode, errorList });
                return null;
            }

            RefundOrderResponseDto responseDto = await ParseJsonAsync<RefundOrderResponseDto>(responseMessage);

            if (responseDto.HasError)
            {
                _log.Error(new { message = "Credit query failed", request, responseDto.ErrorMessage });
                return null;
            }

            var result = _mapper.Map<CreditResponse>(responseDto);

            _log.Information(new { message = "Credit query succeeded", orderNumber = request.OrderNumber, result });

            return result;
        }

        public async Task<CreditResponse> CreditFullAsync(string authorizationKey, string orderNumber)
        {
            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, $"api/v3/orders/{orderNumber}/refunds", HttpMethod.Post, orderNumber);

            //INFO: No POST data needed for full refunds. --tarjei

            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage), true);

            if (responseMessage == null)
            {
                _log.Error(new { message = "Full credit query failed. Service response was NULL or not OK", orderNumber });
                return null;
            }

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                ResponseMessageDto[] errorList = await ParseJsonArrayAsync<ResponseMessageDto>(responseMessage);
                _log.Warning(new { message = "Full credit query faild.", orderNumber, responseMessage.StatusCode, errorList });
                return null;
            }

            RefundOrderResponseDto responseDto = await ParseJsonAsync<RefundOrderResponseDto>(responseMessage);

            if (responseDto.HasError)
            {
                _log.Error(new { message = "Full credit query failed", orderNumber, responseDto.ErrorMessage });
                return null;
            }

            var result = _mapper.Map<CreditResponse>(responseDto);

            _log.Information(new { message = "Full credit query succeeded", orderNumber, result });

            return result;
        }

        public async Task<OrderResponse> GetOrderAsync(string authorizationKey, string orderNumber)
        {
            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, $"api/v3/orders/{orderNumber}", HttpMethod.Get);

            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage), true);

            if (responseMessage == null)
            {
                _log.Error(new { message = "Get order query failed. Service response was NULL or not OK", orderNumber });
                return null;
            }

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                ResponseMessageDto[] errorList = await ParseJsonArrayAsync<ResponseMessageDto>(responseMessage);
                _log.Warning(new { message = "Get order query faild.", orderNumber, responseMessage.StatusCode, errorList });
                return null;
            }

            OrderResponseDto responseDto = await ParseJsonAsync<OrderResponseDto>(responseMessage);

            if (responseDto.HasError)
            {
                _log.Error(new { message = "Get order query failed", orderNumber, responseDto.ErrorMessage });
                return null;
            }

            var result = _mapper.Map<OrderResponse>(responseDto);

            _log.Information(new { message = "Get order query succeeded", orderNumber, result });

            return result;
        }

        public async Task<Version> GetVersionAsync(string authorizationKey)
        {
            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, "api/v3/version", HttpMethod.Get);
            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage));
            if (responseMessage == null)
            {
                _log.Error("Version query failed. Service response was NULL");
                return null;
            }

            VersionInfoDto dto = await ParseJsonAsync<VersionInfoDto>(responseMessage);

            if (dto.HasError)
            {
                _log.Error(new { message = "Version query failed", dto.ErrorMessage });
                return null;
            }

            return dto.Version;
        }

        public bool IsAfterPayOptionUnavailable(string errorCode)
        {
            switch (errorCode)
            {
                case "200.001":
                case "200.002":
                case "200.003":
                case "200.900":
                case "200.901":
                case "200.902":
                case "200.903":
                    return true;
                default:
                    return false;
            }
        }

        public async Task<bool> IsApiUpAsync(string authorizationKey)
        {
            HttpRequestMessage requestMessage = BuildRequest(authorizationKey, "api/v3/status", HttpMethod.Get);
            HttpResponseMessage responseMessage = await CallAsync(() => Client.SendAsync(requestMessage));

            if (responseMessage == null)
            {
                _log.Error("Status query failed. Service response was NULL");
                return false;
            }

            return responseMessage.StatusCode == HttpStatusCode.OK;
        }

        public bool IsMissingSsnError(string errorCode)
        {
            return errorCode?.Equals("400.103", StringComparison.OrdinalIgnoreCase) ?? false;
        }

        public bool IsUserError(string errorCode)
        {
            return !String.IsNullOrEmpty(errorCode) && (errorCode.StartsWith("200.", StringComparison.OrdinalIgnoreCase) || IsMissingSsnError(errorCode));
        }

        private static HttpRequestMessage BuildRequest(string authorizationKey, string address, HttpMethod method, object idempotencyKey = null)
        {
            var request = new HttpRequestMessage { RequestUri = new Uri(Client.BaseAddress, address), Method = method };
            request.Headers.Add("X-Auth-Key", authorizationKey);
            if (idempotencyKey != null)
                request.Headers.Add("X-Idempotency-Key", idempotencyKey.ToString());
            return request;
        }

        private static async Task<string> SerializeDto(object entity)
        {
            var serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), Culture = CultureInfo.InvariantCulture };
            serializerSettings.Converters.Add(new StringEnumConverter());
            return await Task.Factory.StartNew(() => JsonConvert.SerializeObject(entity, serializerSettings));
        }

        private string GetErrorMessage(ResponseMessageDto[] errorList, out string errorCode)
        {
            if (!errorList.Any())
            {
                errorCode = string.Empty;
                return string.Empty;
            }

            ResponseMessageDto userError = errorList.FirstOrDefault(e => IsUserError(e.Code));
            if (userError != null)
            {
                errorCode = userError.Code;
                return userError.CustomerFacingMessage;
            }

            ResponseMessageDto firstError = errorList.First();
            errorCode = firstError.Code;
            return firstError.CustomerFacingMessage;
        }
    }
}
