using Epinova.ArvatoPaymentGateway;
using StructureMap;
using Xunit;
using Xunit.Abstractions;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class InvoiceGatewayRegistryTests
    {
        private readonly Container _container;
        private readonly ITestOutputHelper _output;

        public InvoiceGatewayRegistryTests(ITestOutputHelper output)
        {
            _output = output;
            _container = new Container(new TestableRegistry());
            _container.Configure(x => { x.AddRegistry(new InvoiceGatewayRegistry()); });
        }


        [Fact]
        public void AssertConfigurationIsValid()
        {
            _output.WriteLine(_container.WhatDoIHave());
            _container.AssertConfigurationIsValid();
        }

        [Fact]
        public void Getting_ICustomerLookupService_ReturnsInvoiceGatewayService()
        {
            var instance = _container.GetInstance<ICustomerLookupService>();

            Assert.IsType<InvoiceGatewayService>(instance);
        }


        [Fact]
        public void Getting_IInvoiceGatewayService_ReturnsInvoiceGatewayService()
        {
            var instance = _container.GetInstance<IInvoiceGatewayService>();

            Assert.IsType<InvoiceGatewayService>(instance);
        }
    }
}
