using EPiServer.Logging;
using Moq;
using StructureMap;

namespace Epinova.ArvatoPaymentGatewayTests
{
    internal class TestableRegistry : Registry
    {
        public TestableRegistry()
        {
            For<ILogger>().Use(new Mock<ILogger>().Object);
        }
    }
}