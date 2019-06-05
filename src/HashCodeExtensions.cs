using System.Collections.Generic;
using System.Linq;

namespace Epinova.ArvatoPaymentGateway
{
    internal static class HashCodeExtensions
    {
        public static int GetListHashCode<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
                return 0;

            List<int> candidates = sequence
                .Where(item => item != null)
                .Select(item => item.GetHashCode())
                .ToList();

            return candidates.Any() ? candidates.Aggregate((total, nextCode) => total ^ nextCode) : 0;
        }
    }
}