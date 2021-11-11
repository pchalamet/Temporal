using NUnit.Framework;
using Temporality;

namespace Temporalityity.Tests
{
    public class MergeTests
    {
        [Test]
        public void Test_Merge()
        {
            var temporal = Temporal.Create(new[] { Period.Create(1, 0, 1),
                                                   Period.Create(1, 1, 5),
                                                   Period.Create(3, 7, 10),
                                                   Period.Create(3, 13, 15),
                                                   Period.Create(5, 17, 20),
                                                   Period.Create(5, 20, 200) });

            var expectedTemporal = Temporal.Create(new[] { Period.Create(1, 0, 5),
                                                           Period.Create(3, 7, 10),
                                                           Period.Create(3, 13, 15),
                                                           Period.Create(5, 17, 200) });

            Assert.AreEqual(expectedTemporal, temporal.Merge());
        }
    }
}
