using System;
using System.Collections.Generic;

namespace Timesheet.Micro.Models.Extensions
{
   public static class ListExtender
    {
        public static void SortBy<T>(this List<T> list, Func<T, IComparable> func)
        {
            list.Sort((a, b) => func(a).CompareTo(func(b)));
        }
    }

}
