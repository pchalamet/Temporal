using System;
using System.Linq;
using System.Collections.Generic;

namespace Temporality
{
    public static class TemporalExtensions
    {
        public static Temporal<V, T> Clamp<V, T>(this Temporal<V, T> temporal, T start, T end) 
            where T : IComparable<T>
        {
            if (end.LowerThan(start))
            {
                throw new ArgumentException($"Invalid period range [{start}; {end}[");
            }

            IEnumerable<Period<V, T>> clamp()
            {
                foreach(var period in temporal.Periods)
                {
                    if (period.End.GreaterThan(start) && end.GreaterThan(period.Start))
                    {
                        var newStart = Comparable.Max(period.Start, start);
                        var newEnd = Comparable.Min(period.End, end);
                        yield return Period.Create(period.Value, newStart, newEnd);
                    }
                }
            }

            var newPeriods = clamp();
            return Temporal.Create(newPeriods);
        }

        public static Temporal<V3, T> Combine<V1, V2, V3, T>(this Temporal<V1, T> temporal1, Temporal<V2, T> temporal2, Func<V1, V2, V3> builder)
            where T : IComparable<T>
        {
            IEnumerable<Period<V3, T>> combine(IEnumerable<Period<V1, T>> temporal, Period<V2, T> period)
            {
                foreach (var current in temporal)
                {
                    if (current.End.GreaterThan(period.Start) && current.Start.LowerThan(period.End))
                    {
                        var newValue = builder(current.Value, period.Value);
                        var newStart = Comparable.Max(current.Start, period.Start);
                        var newEnd = Comparable.Min(current.End, period.End);
                        var newPeriod = Period.Create(newValue, newStart, newEnd);
                        yield return newPeriod;
                    }
                }
            }

            var res = temporal2.Periods.SelectMany(period => combine(temporal1.Periods, period));
            return Temporal.Create(res);
        }

        public static bool Coverage<V, T>(this Temporal<V, T> temporal, T start, T end)
            where T : IComparable<T>
        {
            if (end.LowerThan(start))
            {
                throw new ArgumentException($"Invalid period range [{start}; {end}[");
            }

            var currPos = start;
            foreach(var period in temporal.Periods)
            {
                if (! period.Start.Equal(currPos)) {
                    return false;
                }

                currPos = period.End;
            }

            return currPos.Equal(end);
        }

        public static Temporal<V, T> Merge<V, T>(this Temporal<V, T> temporal)
            where V : IEquatable<V>
            where T : IComparable<T>
        {
            IEnumerable<Period<V, T>> merge()
            {
                Period<V, T>? last = default;
                foreach (var current in temporal.Periods)
                {
                    if (last == default)
                    {
                        last = current;
                    }
                    else if (last.End.Equal(current.Start) && last.Value.Equals(current.Value))
                    {
                        last = Period.Create(current.Value, last.Start, current.End);
                    }
                    else
                    {
                        yield return last;
                        last = current;
                    }
                }

                if (last != default)
                {
                    yield return last;
                }
            }

            return Temporal.Create(merge());
        }

        public static bool Validate<V, T>(this Temporal<V, T> temporal)
            where T : IComparable<T>
        {
            T? currPos = default;
            foreach (var period in temporal.Periods)
            {
                if (currPos != null && period.Start.LowerThan(currPos))
                {
                    return false;
                }

                currPos = period.End;
            }

            return true;
        }
    }
}