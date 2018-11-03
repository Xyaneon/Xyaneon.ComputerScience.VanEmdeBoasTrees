using System.Collections.Generic;

namespace Xyaneon.ComputerScience.VanEmdeBoasTrees.Test
{
    /// <summary>
    /// Provides common test data to the rest of this assembly.
    /// </summary>
    internal class TestData
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="TestData"/> class.
        /// </summary>
        static TestData()
        {
            _CLRS_Figure20_6_Values = new List<int>(new int[] { 2, 3, 4, 5, 7, 14, 15 }).AsReadOnly();
        }

        #endregion // End constructors region.

        #region Properties

        /// <summary>
        /// Gets the values used in the CLRS book's Figure 20.6 on
        /// page 548.
        /// </summary>
        public static IReadOnlyList<int> Get_CLRS_Figure20_6_Values()
        {
            return _CLRS_Figure20_6_Values;
        }

        #endregion // End properties region.

        #region Fields

        /// <summary>
        /// Stores the values used in the CLRS book's Figure 20.6 on
        /// page 548.
        /// </summary>
        private static readonly IReadOnlyList<int> _CLRS_Figure20_6_Values;

        #endregion // End fields region.
    }
}
