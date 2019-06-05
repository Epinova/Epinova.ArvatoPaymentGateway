using System.Collections.Generic;
using System.Linq;
using Epinova.ArvatoPaymentGateway;
using Xunit;

namespace Epinova.ArvatoPaymentGatewayTests
{
    public class IdempotentExtensionsTests
    {
        [Fact]
        public void GetIdempotentListKey_InstanceHasItems_ReturnsItemsHashCode()
        {
            IEnumerable<IIdempotent> instance = Enumerable.Range(1, 10).Select(i => new TestableIdempotentModel(i));

            Assert.NotEqual(0, instance.GetIdempotentListKey());
        }

        [Fact]
        public void GetIdempotentListKey_InstanceHasNullEntriesOnly_ReturnsZero()
        {
            IEnumerable<IIdempotent> instance = Enumerable.Range(1, 10).Select(i => (IIdempotent) null);

            Assert.Equal(0, instance.GetIdempotentListKey());
        }

        [Fact]
        public void GetIdempotentListKey_InstanceHasSomeNullItems_ReturnsItemsHashCodeIgnoringNulls()
        {
            IEnumerable<IIdempotent> instance1 = Enumerable.Range(1, 10).Select(i => new TestableIdempotentModel(i));
            IEnumerable<IIdempotent> instance2 = Enumerable.Range(1, 10).Select(i => new TestableIdempotentModel(i)).Concat(Enumerable.Range(1, 10).Select(i => (IIdempotent) null));

            Assert.Equal(instance1.GetIdempotentListKey(), instance2.GetIdempotentListKey());
        }

        [Fact]
        public void GetIdempotentListKey_InstanceIsEmpty_ReturnsZero()
        {
            IEnumerable<IIdempotent> instance = Enumerable.Empty<IIdempotent>();

            Assert.Equal(0, instance.GetIdempotentListKey());
        }

        [Fact]
        public void GetIdempotentListKey_InstanceIsNull_ReturnsZero()
        {
            IEnumerable<IIdempotent> instance = null;

            Assert.Equal(0, instance.GetIdempotentListKey());
        }

        #region Nested type: TestableIdempotentModel

        private class TestableIdempotentModel : IIdempotent
        {
            public TestableIdempotentModel(int key)
            {
                Key = key;
            }

            private int Key { get; }

            public int GetIdempotentKey()
            {
                return Key;
            }
        }

        #endregion
    }
}