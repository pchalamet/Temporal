using System;

namespace Temporality
{
    public static class Comparable
    {
        public static T Min<T>(T t1, T t2) where T : IComparable<T>
        {
            return t1.CompareTo(t2) <= 0 ? t1 : t2;
        }

        public static T Max<T>(T t1, T t2) where T : IComparable<T>
        {
            return t1.CompareTo(t2) <= 0 ? t2 : t1;
        }

        public static bool Equal<T>(this T t1, T t2) where T : IComparable<T>
        {
            return t1.CompareTo(t2) == 0;
        }

        public static bool LowerThan<T>(this T t1, T t2) where T : IComparable<T>
        {
            return t1.CompareTo(t2) < 0;
        }

        public static bool GreaterThan<T>(this T t1, T t2) where T : IComparable<T>
        {
            return t1.CompareTo(t2) > 0;
        }
    }
}