using System;

namespace Monad.Monads
{
    /// <summary>
    /// The class is not contain exactly monads. 
    /// It is my maneuvering area.
    /// Don't open this file any more.
    /// </summary>
    public static class StrangeMonad
    {
        #region With

        public static Person GetManager1(this Building b)
        {
            if (b != null)
                return b.Manager;

            return null;
        }

        public static Person GetManager2(this Building b, Func<Building, Person> action)
        {
            if (b != null)
                return action(b);

            return null;
        }

        public static Person GetManager3(this Building b, Func<Building, Person> action)
        {
            if (b != default(Building))
                return action(b);

            return default(Person);
        }

        public static Contact GetContact(this Person p)
        {
            if (p != null)
                return p.ContactInfo;

            return null;
        }

        public static string GetPhoneNumber(this Contact c)
        {
            if (c != null)
                return c.PhoneNumber;

            return null;
        }

        #endregion

        #region Do

        public static string StringDo(this string source, Action<string> action)
        {
            if (!string.IsNullOrEmpty(source))
                action(source);

            return source;
        }

        #endregion
    }
}