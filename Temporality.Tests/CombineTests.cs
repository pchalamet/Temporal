using System;
using NUnit.Framework;
using Temporality;

namespace Temporalityity.Tests
{
    public class CombineTests
    {
        [Test]
        public void Verify_Combine()
        {
            var temporal1 = Temporal.Create(new[] { Period.Create(1, 0, 1),
                                                    Period.Create(2, 4, 5),
                                                    Period.Create(3, 7, 10),
                                                    Period.Create(4, 13, 15),
                                                    Period.Create(5, 17, 20),
                                                    Period.Create(6, 25, 200) });

            var temporal2 = Temporal.Create(new[] { Period.Create('A', 2, 3),
                                                    Period.Create('B', 5, 6),
                                                    Period.Create('C', 8, 11),
                                                    Period.Create('D', 12, 14),
                                                    Period.Create('E', 16, 21),
                                                    Period.Create('F', 26, 40),
                                                    Period.Create('G', 45, 50),
                                                    Period.Create('H', 80, 100) });

            Func<int, char, string> combinator = (int n, char c) => { return new string(c, n); };

            var expectedTemporal = Temporal.Create(new[] { Period.Create("CCC", 8, 10),
                                                           Period.Create("DDDD", 13, 14),
                                                           Period.Create("EEEEE", 17, 20),
                                                           Period.Create("FFFFFF", 26, 40),
                                                           Period.Create("GGGGGG", 45, 50),
                                                           Period.Create("HHHHHH", 80, 100) });

            var temporal = temporal1.Combine(temporal2, combinator);

            Assert.AreEqual(expectedTemporal, temporal);
        }
    }
}
