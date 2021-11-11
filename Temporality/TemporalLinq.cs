using System;
using System.Collections.Generic;
using System.Linq;

namespace Temporality
{
    public static class TemporalLinqExtensions
    {
        public static Temporal<V2, T> Select<V1, V2, T>(this Temporal<V1, T> temporal, Func<V1, V2> projection)
            where T : IComparable<T>
        {
            var projected = temporal.Periods.Select(x => Period.Create(projection(x.Value), x.Start, x.End));
            return Temporal.Create(projected);
        }

        public static Temporal<V3, T> SelectMany<V1, V2, V3, T>(this Temporal<V1, T> source, Func<Temporal<V1, T>, Temporal<V2, T>> projection, Func<V1, V2, V3> operation)
            where T : IComparable<T>
        {
            return source.Combine(projection(source), operation);
        }

        public static Temporal<V, T> Where<V, T>(this Temporal<V, T> source, Predicate<V> predicate)
            where T : IComparable<T>
        {
            return Temporal.Create(source.Periods.Where(x => predicate(x.Value)));
        }
    }
}