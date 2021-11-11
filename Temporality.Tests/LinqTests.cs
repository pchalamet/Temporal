using System;
using NUnit.Framework;
using Temporality;

namespace Temporalityity.Tests
{
    public class LinqTests
    {
        [Test]
        public void TestSingleFrom()
        {
            var temporal1 = Temporal.Create(new[] { Period.Create(1, 10, 20),
                                                              Period.Create(2, 30, 40) });
            var res = from x in temporal1
                      select x;
        
            var expectedTemporal = Temporal.Create(new[] { Period.Create(1, 10, 20),
                                                           Period.Create(2, 30, 40) });

            Assert.AreEqual(expectedTemporal, res);
        }

        [Test]
        public void TestMultipleFrom()
        {
            var temporal1 = Temporal.Create(new[] { Period.Create(1, 10, 20),
                                                    Period.Create(2, 30, 40) });
            var temporal2 = Temporal.Create(new[] { Period.Create('A', 11, 20),
                                                    Period.Create('B', 30, 39) });

            var res = from x in temporal1
                      from y in temporal2
                      select new string(y, x);

            var expectedTemporal = Temporal.Create(new[] { Period.Create("A", 11, 20),
                                                           Period.Create("BB", 30, 39) });

            Assert.AreEqual(expectedTemporal, res);
        }

        [Test]
        public void TestMultipleFromWhere()
        {
            var temporal1 = Temporal.Create(new[] { Period.Create(1, 10, 20),
                                                    Period.Create(2, 30, 35),
                                                    Period.Create(1, 35, 40) });
            var temporal2 = Temporal.Create(new[] { Period.Create('A', 11, 20),
                                                    Period.Create('B', 30, 39) });
            var temporal3 = Temporal.Create(new[] { Period.Create('*', 11, 20),
                                                    Period.Create('+', 30, 39) });

            // temporal1:    [10----------------------------35[[35-------40[
            //                            2                          3

            // temporal2:      [11------20[       [30-----------------39[
            //                      A                         B

            // temporal2:      [11------20[       [30-----------------39[
            //                      *                         +


            var res = from x in temporal1
                      from y in temporal2
                      from z in temporal3
                      where x == 2 && z == '+'
                      select new string(y, x) + z;

            var mergeRes = res.Merge();

            var expectedTemporal = Temporal.Create(new[] { Period.Create("BB+", 30, 35) });

            Assert.AreEqual(expectedTemporal, mergeRes);
        }

    }


}