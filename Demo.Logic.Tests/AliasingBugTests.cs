namespace Demo.Logic.Tests
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using NUnit.Framework;

    public class AliasingBugTests
    {
        [Test]
        public void TestMutable_ShouldFail()
        {
            var elements = GetElements().ToList();
            var r1 = TestMutable.Sum(elements);
            var r2 = TestMutable.SumAbsolute(elements);
            var r3 = TestMutable.Sum(elements);

            Assert.AreEqual(-10, r1);
            Assert.AreEqual(10, r2);
            Assert.AreEqual(-10, r3);
        }

        [Test]
        public void TestImmutable_ShouldNotFail()
        {
            var elements = GetElements().ToImmutableList();
            var r1 = TestImmutable.Sum(elements);
            var r2 = TestImmutable.SumAbsolute(elements);
            var r3 = TestImmutable.Sum(elements);

            Assert.AreEqual(-10, r1);
            Assert.AreEqual(10, r2);
            Assert.AreEqual(-10, r3);
        }

        private static IEnumerable<int> GetElements()
        {
            return new List<int> {-5, -3, -2};
        }
    }
}
