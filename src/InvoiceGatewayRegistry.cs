using AutoMapper;
using StructureMap;

namespace Epinova.ArvatoPaymentGateway
{
    public class InvoiceGatewayRegistry : Registry
    {
        public InvoiceGatewayRegistry()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new InvoiceGatewayMappingProfile()); });
            mapperConfiguration.CompileMappings();

            ForConcreteType<InvoiceGatewayService>().Configure.Ctor<IMapper>().Is(mapperConfiguration.CreateMapper());
            Forward<InvoiceGatewayService, IInvoiceGatewayService>();
            Forward<InvoiceGatewayService, ICustomerLookupService>();
        }
    }
}
