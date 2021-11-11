using NUnit.Framework;
using Temporality;

namespace Temporalityity.Tests
{
    public class CoverageTests
    {
        [Test]
        public void Empty_Is_Not_Covered()
        {
            Assert.IsFalse(Temporal<int, int>.Empty.Coverage(10, 20));
        }

        [Test]
        public void Atom_Coverage()
        {
            var temporal = Temporal.Create(new[] { Period.Create(42, 10, 20) });

            Assert.IsTrue(temporal.Coverage(10, 20));
            Assert.IsFalse(temporal.Coverage(11, 20));
            Assert.IsFalse(temporal.Coverage(11, 19));
            Assert.IsFalse(temporal.Coverage(9, 20));
            Assert.IsFalse(temporal.Coverage(10, 21));
            Assert.IsFalse(temporal.Coverage(9, 21));
        }
    }
}
