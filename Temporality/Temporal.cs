using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temporality
{
    public class Temporal<V, T> : IEquatable<Temporal<V, T>>
        where T : IComparable<T>
    {
        internal Temporal(IEnumerable<Period<V, T>> periods)
        {
            this.Periods = periods.ToList();
            this.Validate();
        }

        public IEnumerable<Period<V, T>> Periods { get; }

        public static readonly Temporal<V, T> Empty = new Temporal<V,T>(Enumerable.Empty<Period<V, T>>());

        public override string ToString()
        {
            var sb = new StringBuilder();
            return string.Join("\n", this.Periods.Select(x => x.ToString()));
        }

        public bool Equals(Temporal<V, T>? other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Periods.SequenceEqual(other.Periods);
        }
    }

    public static class Temporal
    {
        public static Temporal<V, T> Create<V, T>(IEnumerable<Period<V, T>> periods)
            where T : IComparable<T>
        {
            return new Temporal<V, T>(periods);
        }
    }
}
