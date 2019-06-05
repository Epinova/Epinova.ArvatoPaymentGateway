using System.Collections.Generic;
using System.Linq;
using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class HashCodeExtensionsTests
    {
        [Fact]
        public void GetListHashCode_InstanceHasItems_ReturnsItemsHashCode()
        {
            IEnumerable<int> instance = Enumerable.Range(1, 10);

            Assert.NotEqual(0, instance.GetListHashCode());
        }

        [Fact]
        public void GetListHashCode_InstanceHasNullEntriesOnly_ReturnsZero()
        {
            IEnumerable<object> instance = Enumerable.Range(1, 10).Select(i => (object) null);

            Assert.Equal(0, instance.GetListHashCode());
        }

        [Fact]
        public void GetListHashCode_InstanceHasSomeNullItems_ReturnsItemsHashCodeIgnoringNulls()
        {
            IEnumerable<int> instance1 = Enumerable.Range(1, 10);
            IEnumerable<object> instance2 = Enumerable.Range(1, 10).Cast<object>().Concat(Enumerable.Range(1, 10).Select(i => (object) null));

            Assert.Equal(instance1.GetListHashCode(), instance2.GetListHashCode());
        }

        [Fact]
        public void GetListHashCode_InstanceIsEmpty_ReturnsZero()
        {
            IEnumerable<object> instance = Enumerable.Empty<object>();

            Assert.Equal(0, instance.GetListHashCode());
        }

        [Fact]
        public void GetListHashCode_InstanceIsNull_ReturnsZero()
        {
            IEnumerable<object> instance = null;

            Assert.Equal(0, instance.GetListHashCode());
        }
    }
}