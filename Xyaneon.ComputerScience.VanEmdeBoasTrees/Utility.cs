using System;

namespace Xyaneon.ComputerScience.VanEmdeBoasTrees
{
    /// <summary>
    /// Provides helper methods to the rest of this assembly.
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// Determines whether a given <paramref name="value"/> is a
        /// power of two.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is a
        /// power of two; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// The following check is based on this:
        /// https://stackoverflow.com/a/600306
        /// </remarks>
        public static bool IsPowerOfTwo(int value)
        {
            if (value == 0)
            {
                return false;
            }
            return (value & (value - 1)) == 0;
        }

        /// <summary>
        /// Computes the binary log of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// The value to compute the binary log of.
        /// </param>
        /// <returns>
        /// The binary log of <paramref name="value"/>.
        /// </returns>
        public static double Lg(double value)
        {
            return Math.Log(value, 2);
        }

        /// <summary>
        /// Computes the lower square root of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// The value to compute the lower square root of.
        /// </param>
        /// <returns>
        /// The lower square root of <paramref name="value"/>.
        /// </returns>
        /// <remarks>
        /// This method is based upon the formula for the lower square root
        /// given in the CLRS book on page 546.
        /// </remarks>
        /// <seealso cref="UpperSquareRoot(int)"/>
        public static double LowerSquareRoot(int value)
        {
            return Math.Pow(2, Math.Floor(Lg(value) / 2));
        }

        /// <summary>
        /// Computes the upper square root of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// The value to compute the upper square root of.
        /// </param>
        /// <returns>
        /// The upper square root of <paramref name="value"/>.
        /// </returns>
        /// <remarks>
        /// This method is based upon the formula for the upper square root
        /// given in the CLRS book on page 546.
        /// </remarks>
        /// <seealso cref="LowerSquareRoot(int)"/>
        public static double UpperSquareRoot(int value)
        {
            return Math.Pow(2, Math.Ceiling(Lg(value) / 2));
        }
    }
}
