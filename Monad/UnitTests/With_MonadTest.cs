using NUnit.Framework;

namespace Monad.UnitTests
{
    public class With_MonadTest
    {
        [Test]
        public void without_monad()
        {
            Building building = BuildingFactory.AtWorkNotNullBuilding();
            string phoneNumber = null;

            if (building != null)
            {
                if (building.Manager != null)
                {
                    if (building.Manager.ContactInfo != null)
                    {
                        if (building.Manager.ContactInfo.PhoneNumber != null)
                        {
                            phoneNumber = building.Manager.ContactInfo.PhoneNumber;
                        }
                    }
                }
            }

            Assert.AreEqual("1234", phoneNumber);
        }

        [Test]
        public void monad_with_not_null()
        {
            string phoneNumber = BuildingFactory.AtWorkNotNullBuilding()
                .With(b => b.Manager)
                .With(m => m.ContactInfo)
                .With(c => c.PhoneNumber);

            Assert.AreEqual("1234", phoneNumber);
        }

        [Test]
        public void my_strange_monad_1()
        {
            string phoneNumber = BuildingFactory.AtWorkNotNullBuilding()
                .GetManager1()
                .GetContact()
                .GetPhoneNumber();

            Assert.AreEqual("1234", phoneNumber);
        }

        [Test]
        public void my_strange_monad_2()
        {
            string phoneNumber = BuildingFactory.AtWorkNotNullBuilding()
                .GetManager2(b => b.Manager)
                .GetContact()
                .GetPhoneNumber();

            Assert.AreEqual("1234", phoneNumber);
        }

        [Test]
        public void my_strange_monad_3()
        {
            string phoneNumber = BuildingFactory.AtWorkNotNullBuilding()
                .GetManager3(b => b.Manager)
                .GetContact()
                .GetPhoneNumber();

            Assert.AreEqual("1234", phoneNumber);
        }
    }
}