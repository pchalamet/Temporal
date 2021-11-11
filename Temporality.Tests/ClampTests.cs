using NUnit.Framework;
using Temporality;

namespace Temporalityity.Tests
{
    public class ClampTests
    {
        [Test]
        public void Empty_Is_Always_Empty()
        {
            var temporal = Temporal<int, int>.Empty;

            var expectedTemporal = Temporal<int, int>.Empty;

            var clampTemporal = temporal.Clamp(10, 20);

            Assert.AreEqual(expectedTemporal, clampTemporal);
        }


        [Test]
        public void Remove_Values_Outside_Clamp()
        {
            var temporal = Temporal.Create(new[] { Period.Create(42, 1, 10),
                                                   Period.Create(666, 20, 30) });

            var expectedTemporal = Temporal<int, int>.Empty;

            var clampTemporal = temporal.Clamp(10, 20);

            Assert.AreEqual(expectedTemporal, clampTemporal);
        }

        [Test]
        public void Start_Must_Clamp()
        {
            var temporal = Temporal.Create(new[] { Period.Create(42, 5, 15),
                                                   Period.Create(666, 20, 30) });

            var expectedTemporal = Temporal.Create(new[] { Period.Create(42, 10, 15) });

            var clampTemporal = temporal.Clamp(10, 20);

            Assert.AreEqual(expectedTemporal, clampTemporal);
        }


        [Test]
        public void End_Must_Clamp()
        {
            var temporal = Temporal.Create(new[] { Period.Create(42, 5, 15),
                                                   Period.Create(666, 18, 30) });

            var expectedTemporal = Temporal.Create(new[] { Period.Create(42, 10, 15),
                                                           Period.Create(666, 18, 20) });

            var clampTemporal = temporal.Clamp(10, 20);

            Assert.AreEqual(expectedTemporal, clampTemporal);
        }
    }
}

