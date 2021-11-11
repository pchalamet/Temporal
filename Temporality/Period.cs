using System;

namespace Temporality
{
    public class Period<V, T> : IEquatable<Period<V, T>>
        where T : IComparable<T>
    {
        internal Period(V value, T start, T end)
        {
            this.Value = value;
            this.Start = start;
            this.End = end;

            if (start.GreaterThan(end))
            {
                throw new ArgumentException($"Invalid period range [{start}; {end}[");
            }
        }

        public V Value { get; }

        public T Start { get; }

        public T End { get; }

        public bool Equals(Period<V, T>? other)
        {
            if (other == null)
            {
                return false;
            }

            // NOTE: this.Value is never null since V can't be null.
            //       Only Object.Equals() can lead to this but since V can't be null we are safe.
            return this.Value!.Equals(other.Value)
                   && this.Start.Equal(other.Start)
                   && this.End.Equal(other.End);
        }

        public override string ToString()
        {
            return $"[{this.Start}; {this.End}[ = {Value}";
        }
    }

    public static class Period
    {
        public static Period<V, T> Create<V, T>(V value, T start, T end)
            where T : IComparable<T>
        {
            return new Period<V, T>(value, start, end);
        }
    }
}
