using System.Collections.Generic;

namespace Timesheet.Micro.Models.Extensions
{
    public static class IntExtensions
    {
        public static IEnumerable<int> Till(this int startPoint, int stopPoint)
        {
            if (startPoint < stopPoint)
            {
                for (var i = startPoint; i < stopPoint; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (var i = startPoint; i < stopPoint; i--)
                {
                    yield return i;
                }
            }
        }
    }
}
