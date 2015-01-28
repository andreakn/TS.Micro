using System.Collections.Generic;
using System.Linq;

namespace Timesheet.Micro.Models.Extensions
{
    public static class EnumerableExtensions
    {
        public static EnumerableItemOf<T> Of<T>(this IEnumerable<T> enumerable, T item)
        {
            if (enumerable != null && enumerable.Contains(item))
            {
                return new EnumerableItemOf<T>(item);
            }
            return new EnumerableItemOf<T>(default(T));
        }
    }

    public class EnumerableItemOf<T>
    {
        internal EnumerableItemOf(T Value)
        {
            Item = Value;
        }

        public T Item { get; private set; }
    }
}
