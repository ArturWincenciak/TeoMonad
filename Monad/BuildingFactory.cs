namespace Monad
{
    public class Building
    {
        public Person Manager { get; set; }
    }

    public class Person
    {
        public Contact ContactInfo { get; set; }

        public bool IsAtWork { get; set; }
    }

    public class Contact
    {
        public string PhoneNumber { get; set; }
    }

    public static class BuildingFactory
    {
        /// <summary>
        /// Jest w pracy i posiada numer telefonu;
        /// </summary>
        public static Building AtWorkNotNullBuilding()
        {
            return new Building
            {
                Manager = new Person
                {
                    ContactInfo = new Contact
                    {
                        PhoneNumber = "1234"
                    },

                    IsAtWork = true
                }
            };
        }

        /// <summary>
        /// Jest w pracy ale nie posiada numeru telefonu.
        /// </summary>
        public static Building AtWorkNullPhoneNumber()
        {
            return new Building
            {
                Manager = new Person
                {
                    ContactInfo = new Contact
                    {
                        PhoneNumber = null
                    },

                    IsAtWork = true
                }
            };
        }

        /// <summary>
        /// Nie jest w pracy i posiada numer telefonu.
        /// </summary>
        public static Building NotAtWorkNotNullBuilding()
        {
            return new Building
            {
                Manager = new Person
                {
                    ContactInfo = new Contact
                    {
                        PhoneNumber = "1234"
                    },

                    IsAtWork = false
                }
            };
        }

        /// <summary>
        /// Nie jest w pracy i nie posiada numeru telefonu.
        /// </summary>
        public static Building NotAtWorkNullPhoneNumber()
        {
            return new Building
            {
                Manager = new Person
                {
                    ContactInfo = new Contact
                    {
                        PhoneNumber = null
                    },

                    IsAtWork = false
                }
            };
        }
    }
}