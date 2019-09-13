using System;
using System.Linq;
using AutoMapper;

namespace Epinova.ArvatoPaymentGateway
{
    internal class InvoiceGatewayMappingProfile : Profile
    {
        public InvoiceGatewayMappingProfile()
        {
            AllowNullCollections = false;

            MapToDto();
            MapFromDto();
        }

        private void MapFromDto()
        {
            CreateMap<AuthorizeResponseDto, AuthorizeResponse>()
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber))
                .ForMember(dest => dest.AddressList, opt => opt.MapFrom(src => src.Customer.AddressList))
                .ForMember(dest => dest.IsAuthorized, opt => opt.MapFrom(src => src.Outcome == OutcomeTypeDto.Accepted))
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                .ForMember(dest => dest.ErrorCode, opt => opt.Ignore())
                .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore());

            CreateMap<AddressDto, Address>();

            CreateMap<ResponseMessageDto, ResponseMessage>();

            CreateMap<CancelResponseDto, CancelResponse>();

            CreateMap<CaptureResponseDto, CaptureResponse>()
                .ForMember(dest => dest.UnauthorizedAmount, opt => opt.MapFrom(src => src.RemainingAuthorizedAmount));

            CreateMap<RefundOrderResponseDto, CreditResponse>()
                .ForMember(dest => dest.CreditNumbers, opt => opt.MapFrom(src => src.RefundNumbers));

            Func<AddressDto[], CustomerLookupResponse> addressConverter = srcList =>
            {
                AddressDto src = srcList?.FirstOrDefault();
                var dest = new CustomerLookupResponse();
                if (src == null)
                    return dest;

                dest.City = src.City;
                dest.PostalCode = src.PostalCode;
                dest.Street = src.Street;
                return dest;
            };

            CreateMap<UserProfileDto, CustomerLookupResponse>(MemberList.None)
                .ConstructUsing(src => addressConverter(src.AddressList));

            CreateMap<OrderItemExtendedDto, OrderItem>();
            CreateMap<OrderResponseDto, OrderResponse>();
            CreateMap<ResponseOrderDto, OrderDetails>();
            CreateMap<CaptureDto, Capture>();
            CreateMap<RefundDto, Refund>();
            CreateMap<CancellationDto, Cancellation>();
            CreateMap<AvailableInstallmentPlansResponseDto, AvailableInstallmentPlansResponse>()
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                .ForMember(dest => dest.ErrorCode, opt => opt.Ignore())
                .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore());
            CreateMap<InstallmentInfoDto, InstallmentInfo>();
        }

        private void MapToDto()
        {
            CreateMap<AuthorizeRequest, AuthorizeRequestDto>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<AccountProduct, AccountProductDto>();
            CreateMap<Installment, InstallmentDto>();

            CreateMap<AuthorizeCustomer, CustomerRequestDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.CustomerCategory, opt => opt.MapFrom(src => CustomerCategoryDto.Person))
                .ForMember(dest => dest.ConversationLanguage, opt => opt.MapFrom(src => ConversationLanguageDto.NO))
                .ForMember(dest => dest.CustomerNumber, opt => opt.Ignore())
                .ForMember(dest => dest.MobilePhone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.IdentificationNumber, opt => opt.MapFrom(src => src.Identifier));

            CreateMap<AuthorizeCustomer, AddressDto>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.StreetNumber, opt => opt.Ignore())
                .ForMember(dest => dest.CountryCode, opt => opt.Ignore());

            CreateMap<OrderInfo, OrderInfoDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.LineItems));

            CreateMap<CreditRequest, RefundOrderRequestDto>()
                .ForMember(dest => dest.RefundType, opt => opt.MapFrom(src => RefundTypeDto.Return))
                .ForMember(dest => dest.ParentTransactionReference, opt => opt.Ignore());

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.GroupId, opt => opt.Ignore())
                .ForMember(dest => dest.UnitCode, opt => opt.Ignore())
                .ForMember(dest => dest.ProductUrl, opt => opt.Ignore())
                .ForMember(dest => dest.MarketPlaceSellerId, opt => opt.Ignore());

            CreateMap<OrderItem, RefundOrderItemDto>()
                .IncludeBase<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.RefundType, opt => opt.MapFrom(src => RefundTypeDto.Return));

            CreateMap<CaptureRequest, CaptureRequestDto>()
                .ForMember(dest => dest.CampaignNumber, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingDetails, opt => opt.Ignore());
            CreateMap<OrderSummary, OrderSummaryDto>();

            CreateMap<AvailableInstallmentPlansRequest, AvailableInstallmentPlansRequestDto>()
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => CurrencyDto.NOK))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => CountryCodeDto.NO));
            CreateMap<CheckoutCustomer, CheckoutCustomerDto>()
                .ForMember(dest => dest.CustomerCategory, opt => opt.MapFrom(src => CustomerCategoryDto.Person))
                .ForMember(dest => dest.ConversationLanguage, opt => opt.MapFrom(src => ConversationLanguageDto.NO))
                .ForMember(dest => dest.CustomerNumber, opt => opt.Ignore())
                .ForMember(dest => dest.MobilePhone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.BirthDate, opt => opt.Ignore())
                .ForMember(dest => dest.RiskData, opt => opt.Ignore());
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.CountryCode, opt => opt.Ignore())
                .ForMember(dest => dest.StreetNumber, opt => opt.Ignore());
        }
    }
}