using System.Collections.Generic;
using System.Linq;

namespace Epinova.ArvatoPaymentGateway
{
    internal static class IdempotentExtensions
    {
        public static int GetIdempotentListKey(this IEnumerable<IIdempotent> sequence)
        {
            if (sequence == null)
                return 0;

            List<int> candidates = sequence
                .Where(item => item != null)
                .Select(item => item.GetIdempotentKey())
                .ToList();

            return candidates.Any() ? candidates.Aggregate((total, nextCode) => total ^ nextCode) : 0;
        }
    }
}