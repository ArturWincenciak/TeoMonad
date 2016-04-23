using FakeItEasy;
using NUnit.Framework;

namespace Monad.UnitTests
{
    public class Do_MonadTest
    {
        [Test]
        public void without_monad()
        {
            Building building = BuildingFactory.AtWorkNotNullBuilding();
            IPhone phone = A.Fake<IPhone>();

            if (building != null)
            {
                if (building.Manager != null)
                {
                    if (building.Manager.ContactInfo != null)
                    {
                        if (building.Manager.ContactInfo.PhoneNumber != null)
                        {
                            string phoneNumber = building.Manager.ContactInfo.PhoneNumber;
                            phone.Call(phoneNumber);
                        }
                    }
                }
            }

            A.CallTo(() => phone.Call("1234")).MustHaveHappened();
        }

        [Test]
        public void monad_with_not_null()
        {
            IPhone phone = A.Fake<IPhone>();

            BuildingFactory.AtWorkNotNullBuilding()
                .With(b => b.Manager)
                .With(m => m.ContactInfo)
                .With(c => c.PhoneNumber)
                .Do(phoneNumber => phone.Call(phoneNumber));

            A.CallTo(() => phone.Call("1234")).MustHaveHappened();
        }

        [Test]
        public void monad_with_null()
        {
            IPhone phone = A.Fake<IPhone>();

            BuildingFactory.AtWorkNullPhoneNumber()
                .With(b => b.Manager)
                .With(m => m.ContactInfo)
                .With(c => c.PhoneNumber)
                .Do(phoneNumber => phone.Call(phoneNumber));

            A.CallTo(() => phone.Call(null)).WithAnyArguments().MustNotHaveHappened();
        }

        [Test]
        public void my_strange_monad()
        {
            IPhone phone = A.Fake<IPhone>();

            BuildingFactory.AtWorkNotNullBuilding()
                .GetManager1()
                .GetContact()
                .GetPhoneNumber()
                .StringDo(phoneNumber => phone.Call(phoneNumber));

            A.CallTo(() => phone.Call("1234")).MustHaveHappened();
        }
    }
}