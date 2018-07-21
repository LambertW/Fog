﻿using System;
using System.Collections.Generic;

namespace Fog.Collections.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.Contains(item))
                return false;

            source.Add(item);
            return true;
        }
    }
}
