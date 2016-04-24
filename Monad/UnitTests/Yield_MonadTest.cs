using System.Collections.Generic;
using FakeItEasy;
using Monad.Monads;
using NUnit.Framework;

namespace Monad.UnitTests
{
    public class Yield_MonadTest
    {
        public class SomethingForTesting
        {
            /// <summary>
            /// Simply return a collection of strings.
            /// </summary>
            public IEnumerable<string> GetFromSetX()
            {
                yield return "SetXElement1";
                yield return "SetXElement2";
                yield return "SetXElement3";
            }

            /// <summary>
            /// Simply return a difirent collection of strings.
            /// </summary>
            public IEnumerable<string> GetFromSetY()
            {
                yield return "SetYElement1";
                yield return "SetYElement2";
                yield return "SetYElement3";
            }

            /// <summary>
            /// 
            /// This method you can't compile. 
            /// 
            /// This is an example of how I want to write my code 
            /// when I want to return many collctions.
            /// 
            /// Below is the correct implementation of this case.
            /// 
            /// I've did it by functional programing paradigm.
            /// 
            /// </summary>
            //public IEnumerable<string> GetAllElements_YOU_CANNOT_DO_THAT()
            //{
            //    yield return "AdditionalElement";
            //    yield return GetFromSetX();
            //    yield return GetFromSetY();
            //}

            /// <summary>
            /// 
            /// This method is the target for testing.
            /// It is an client of funcional programming.
            /// It is a new version of the above methods.
            /// 
            /// </summary>
            public IEnumerable<string> GetAllElements()
            {
                return "AdditionalElement"
                    .Yield()
                    .Yield(GetFromSetX)
                    .Yield(GetFromSetY);
            }

            /// <summary>
            /// 
            /// This method is also the target for testing.
            /// It is an also client of funcional programming.
            /// It is a little different version of the above methods.
            /// 
            /// </summary>
            public IEnumerable<string> GetAllElements2()
            {
                return _.Yield("AdditionalElement")
                    .Yield(GetFromSetX)
                    .Yield(GetFromSetY);
            }
        }

        [Test]
        public void how_to_use_the_yeld_monad_1()
        {
            ILogger tester = A.Fake<ILogger>();
            SomethingForTesting obj = new SomethingForTesting();
            IEnumerable<string> enumerable = obj.GetAllElements();

            foreach (var item in enumerable)
                tester.Log(item);

            A.CallTo(() => tester.Log("AdditionalElement")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetXElement1")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetXElement2")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetXElement3")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetYElement1")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetYElement2")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetYElement3")).MustHaveHappened();
        }

        [Test]
        public void how_to_use_the_yeld_monad_2()
        {
            ILogger tester = A.Fake<ILogger>();
            SomethingForTesting obj = new SomethingForTesting();

            IEnumerable<string> enumerable =
                _.Yield("AdditionalElement")
                .Yield(obj.GetFromSetX)
                .Yield(obj.GetFromSetY);

            foreach (var item in enumerable)
                tester.Log(item);

            A.CallTo(() => tester.Log("AdditionalElement")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetXElement1")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetXElement2")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetXElement3")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetYElement1")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetYElement2")).MustHaveHappened();
            A.CallTo(() => tester.Log("SetYElement3")).MustHaveHappened();
        }
    }
}