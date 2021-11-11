using NUnit.Framework;
using Temporality;

namespace Temporalityity.Tests
{
    public class ComparableTests
    {
        [Test]
        public void Compute_Max()
        {
            Assert.AreEqual(20, Comparable.Max(10, 20));
        }

        [Test]
        public void Compute_Min()
        {
            Assert.AreEqual(10, Comparable.Min(10, 20));
        }

        [Test]
        public void Compute_Equal()
        {
            Assert.IsTrue(10.Equal(10));
            Assert.IsFalse(10.Equal(20));
        }

        [Test]
        public void Compute_LowerThan()
        {
            Assert.IsTrue(10.LowerThan(20));
            Assert.IsFalse(10.LowerThan(10));
        }

        [Test]
        public void Compute_GreaterThan()
        {
            Assert.IsTrue(20.GreaterThan(10));
            Assert.IsFalse(10.GreaterThan(10));
        }
    }
}
