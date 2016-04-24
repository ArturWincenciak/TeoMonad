using System;
using System.Collections.Generic;

namespace Monad.Monads
{
    public static class MonadYeld
    {
        public static IEnumerable<T> Yield<T>(this T source)
        {
            yield return source;
        }

        public static IEnumerable<T> Yield<T>(this IEnumerable<T> source, Func<IEnumerable<T>> additionals)
        {
            if (default(IEnumerable<T>) != source)
                foreach (var item in source)
                    yield return item;

            var add = additionals();
            if (default(IEnumerable<T>) != add)
                foreach (var item in add)
                    yield return item;
        }
    }
}