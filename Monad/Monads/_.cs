using System;
using System.Collections.Generic;

namespace Monad.Monads
{
    public static class _
    {
        public static IEnumerable<T> Yield<T>(T source)
        {
            yield return source;
        }

        public static IEnumerable<T> Yield<T>(Func<IEnumerable<T>> source)
        {
            return source();
        }
    }
}