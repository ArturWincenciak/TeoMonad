using System;

namespace Monad.Monads
{
    public static class Monad
    {
        public static TResult With<TSource, TResult>(this TSource source, Func<TSource, TResult> action)
            where TSource : class
        {
            if (source != default(TSource))
                return action(source);

            return default(TResult);
        }

        public static TSource Do<TSource>(this TSource source, Action<TSource> action)
            where TSource : class
        {
            if (source != default(TSource))
                action(source);

            return source;
        }

        public static TSource If<TSource>(this TSource source, Func<TSource, bool> condition)
            where TSource : class
        {
            if (source != default(TSource) && condition(source))
                return source;

            return default(TSource);
        }

        public static T Log<T>(this T currentValue, string message)
            where T : class
        {
            if (currentValue != default(T))
            {
                ILogger logger = new ConsoleLogger();
                logger.Log(currentValue + " > " + message);
            }

            return currentValue;
        }

        public static T Log<T>(this T currentValue, ILogger logger, string message)
            where T : class
        {
            if (currentValue != default(T))
                logger.Log(currentValue + " > " + message);

            return currentValue;
        }

    }
}