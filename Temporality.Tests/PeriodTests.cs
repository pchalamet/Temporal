using System;
using NUnit.Framework;
using Temporality;

namespace Temporalityity.Tests
{
    public class PeriodTests
    {
        [Test]
        public void TestPeriodConstructor()
        {
            Assert.Throws<ArgumentException>(() => Period.Create(666, 10, 1));
        }
    }
}
