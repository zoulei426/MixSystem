using System;

namespace Mix.Core
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 容差
        /// </summary>
        private const double Tolerance = 1e-6;

        public static T ConvertTo<T>(this object value)
        {
            return typeof(T).IsValueType && value != null
                ? (T)Convert.ChangeType(value, typeof(T))
                : value is T typeValue ? typeValue : default;
        }

        public static bool EqualsWithinTolerance(this double @this, double other)
        {
            return Math.Abs(@this - other) < Tolerance;
        }

        public static bool GreaterOrEqual(this double @this, double other)
        {
            return @this > other || @this.EqualsWithinTolerance(other);
        }

        public static bool LessOrEqual(this double @this, double other)
        {
            return @this < other || @this.EqualsWithinTolerance(other);
        }
    }
}