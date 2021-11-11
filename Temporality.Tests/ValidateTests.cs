using NUnit.Framework;
using Temporality;

namespace Temporalityity.Tests
{
    public class ValidateTests
    {
        [Test]
        public void TestEmptyTemporal()
        {
            Assert.IsTrue(Temporal<int, int>.Empty.Validate());
        }

        [Test]
        public void TestSingleElement()
        {
            var temporal = Temporal.Create(new[] { Period.Create(1, 10, 20) });
            Assert.IsTrue(temporal.Validate());
        }

        [Test]
        public void TestContiguousElements()
        {
            var temporal = Temporal.Create(new[] { Period.Create(1, 10, 20),
                                                   Period.Create(1, 20, 30) });
            Assert.IsTrue(temporal.Validate());
        }

        [Test]
        public void TestElementsWithHoles()
        {
            var temporal = Temporal.Create(new[] { Period.Create(1, 10, 20),
                                                   Period.Create(1, 30, 40),
                                                   Period.Create(2, 50, 70) });
            Assert.IsTrue(temporal.Validate());
        }

        [Test]
        public void TestElementsWithOverlaps()
        {
            var temporal = Temporal.Create(new[] { Period.Create(1, 10, 20),
                                                   Period.Create(1, 15, 40),
                                                   Period.Create(2, 35, 70) });
            Assert.IsFalse(temporal.Validate());
        }

        [Test]
        public void TestInvalidPeriod()
        {
            var temporal = Temporal.Create(new[] { Period.Create(1, 30, 40),
                                                   Period.Create(1, 10, 20),
                                                   Period.Create(2, 50, 70) });
            Assert.IsFalse(temporal.Validate());
        }
    }
}