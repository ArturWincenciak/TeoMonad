using FakeItEasy;
using NUnit.Framework;

namespace Monad.UnitTests
{
    public class If_MonadTest
    {
        [Test]
        public void without_monad_at_work()
        {
            Building building = BuildingFactory.AtWorkNotNullBuilding();
            IPhone phone = A.Fake<IPhone>();

            if (building != null)
            {
                if (building.Manager != null && building.Manager.IsAtWork)
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
        public void monad_with_not_null_at_work()
        {
            IPhone phone = A.Fake<IPhone>();

            BuildingFactory.AtWorkNotNullBuilding()
                .With(b => b.Manager)
                .If(m => m.IsAtWork)
                .With(m => m.ContactInfo)
                .With(c => c.PhoneNumber)
                .Do(phoneNumber => phone.Call(phoneNumber));

            A.CallTo(() => phone.Call("1234")).MustHaveHappened();
        }

        [Test]
        public void monad_with_not_null_not_at_work()
        {
            IPhone phone = A.Fake<IPhone>();

            BuildingFactory.NotAtWorkNotNullBuilding()
                .With(b => b.Manager)
                .If(m => m.IsAtWork)
                .With(m => m.ContactInfo)
                .With(c => c.PhoneNumber)
                .Do(phoneNumber => phone.Call(phoneNumber));

            A.CallTo(() => phone.Call("1234")).MustNotHaveHappened();
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
    }
}