﻿using System.Collections.Generic;
using System.Linq;

namespace Hoist.Extensions
{
    public static class ArrayExtensions
    {
        public static void Deconstruct<T>(this IList<T> list, out T first, out IList<T> rest) {

            first = list.Count > 0 ? list[0] : default;
            rest = list.Skip(1).ToList();
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out IList<T> rest) {
            first = list.Count > 0 ? list[0] : default;
            second = list.Count > 1 ? list[1] : default;
            rest = list.Skip(2).ToList();
        }
    }
}
