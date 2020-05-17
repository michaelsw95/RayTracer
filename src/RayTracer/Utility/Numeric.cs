using System;

namespace RayTracer.Utility
{
    public static class Numeric
    {
        public static bool FloatIsEqual(float a, float b)
        {
            const float EPSILON = 0.00001F;

            return Math.Abs(a - b) < EPSILON;
        }

        public static T Clamp<T>(T lower, T higher, T value) where T : IComparable
        {
            if (value.CompareTo(higher) > 0)
            {
                value = higher;
            }
            else if (value.CompareTo(lower) < 0)
            {
                value = lower;
            }

            return value;
        }

        public static bool IsWithinRange<T>(T lower, T higher, T value) where T : IComparable
        {
            if (value.CompareTo(higher) > 0 || value.CompareTo(lower) < 0)
            {
                return false;
            }

            return true;
        }
    }
}
