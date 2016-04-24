using System;
using System.Diagnostics;
using FakeItEasy;
using Monad.Monads;
using NUnit.Framework;

namespace Monad.UnitTests
{
    public class Log_MonadTest
    {
        [Test]
        public void without_monad_at_work()
        {
            Building building = BuildingFactory.AtWorkNotNullBuilding();
            IPhone phone = A.Fake<IPhone>();

            if (building != null)
            {
                if (building.Manager != null)
                {
                    Console.WriteLine("Found Manager");
                    if (building.Manager.IsAtWork)
                    {
                        Console.WriteLine("Manager is at work");
                        if (building.Manager.ContactInfo != null)
                        {
                            if (building.Manager.ContactInfo.PhoneNumber != null)
                            {
                                Console.WriteLine("Dialing The Manager");
                                phone.Call(building.Manager.ContactInfo.PhoneNumber);
                            }
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
                .Log("Found Manager")
                .If(m => m.IsAtWork)
                .Log("Manager is at work")
                .With(m => m.ContactInfo)
                .With(c => c.PhoneNumber)
                .Log("Dialing The Manager")
                .Do((p) => phone.Call(p));

            A.CallTo(() => phone.Call("1234")).MustHaveHappened();
        }

        [Test]
        public void monad_with_not_null_at_work_with_logger_inside()
        {
            IPhone phone = A.Fake<IPhone>();
            ILogger logger = A.Fake<ILogger>();

            BuildingFactory.AtWorkNotNullBuilding()
                .With(b => b.Manager)
                .Log(logger, "Found Manager")
                .If(m => m.IsAtWork)
                .Log(logger, "Manager is at work")
                .With(m => m.ContactInfo)
                .With(c => c.PhoneNumber)
                .Log(logger, "Dialing The Manager")
                .Do((p) => phone.Call(p));

            A.CallTo(() => phone.Call("1234")).MustHaveHappened();
            A.CallTo(() => logger.Log("Monad.Person > Found Manager")).MustHaveHappened();
            A.CallTo(() => logger.Log("Monad.Person > Manager is at work")).MustHaveHappened();
            A.CallTo(() => logger.Log("1234 > Dialing The Manager")).MustHaveHappened();
        }

        [Test]
        public void monad_with_not_null_not_at_work()
        {
            IPhone phone = A.Fake<IPhone>();

            BuildingFactory.NotAtWorkNotNullBuilding()
                .With(b => b.Manager)
                .Log("Found Manager")
                .If(m => m.IsAtWork)
                .Log("Manager is at work")
                .With(m => m.ContactInfo)
                .With(c => c.PhoneNumber)
                .Log("Dialing The Manager")
                .Do((p) => phone.Call(p));

            A.CallTo(() => phone.Call("1234")).MustNotHaveHappened();
        }

        [Test]
        public void monad_with_null()
        {
            IPhone phone = A.Fake<IPhone>();

            BuildingFactory.AtWorkNullPhoneNumber()
                .With(b => b.Manager)
                .Log("Found Manager")
                .If(m => m.IsAtWork)
                .Log("Manager is at work")
                .With(m => m.ContactInfo)
                .With(c => c.PhoneNumber)
                .Log("Dialing The Manager")
                .Do((p) => phone.Call(p));

            A.CallTo(() => phone.Call(null)).WithAnyArguments().MustNotHaveHappened();
        }
    }
}