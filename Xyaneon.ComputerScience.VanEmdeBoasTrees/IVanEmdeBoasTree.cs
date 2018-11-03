namespace Xyaneon.ComputerScience.VanEmdeBoasTrees
{
    /// <summary>
    /// Exposes properties and methods on classes which represent
    /// a van Emde Boas tree.
    /// </summary>
    public interface IVanEmdeBoasTree
    {
        /// <summary>
        /// Gets the maximum value stored in this tree.
        /// </summary>
        int? Maximum { get; }

        /// <summary>
        /// Gets the minimum value stored in this tree.
        /// </summary>
        int? Minimum { get; }

        /// <summary>
        /// Gets the size of the universe for this node.
        /// </summary>
        int Universe { get; }

        /// <summary>
        /// Deletes <paramref name="value"/> from this subtree.
        /// </summary>
        /// <param name="value">
        /// The value to delete.
        /// </param>
        void Delete(int value);

        /// <summary>
        /// Inserts <paramref name="value"/> into this subtree.
        /// </summary>
        /// <param name="value">
        /// The value to insert.
        /// </param>
        void Insert(int value);

        /// <summary>
        /// Determines whether <paramref name="value"/> is contained
        /// in this subtree.
        /// </summary>
        /// <param name="value">
        /// The value to look for.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is
        /// contained in this subtree; otherwise, <see langword="false"/>.
        /// </returns>
        bool Member(int value);

        /// <summary>
        /// Determines the predecessor of <paramref name="value"/> in this
        /// subtree.
        /// </summary>
        /// <param name="value">
        /// The value to find the predecessor of.
        /// </param>
        /// <returns>
        /// The predecessor of <paramref name="value"/> in this subtree if
        /// currently stored; otherwise, <see langword="null"/>.
        /// </returns>
        int? Predecessor(int value);

        /// <summary>
        /// Determines the successor of <paramref name="value"/> in this
        /// subtree.
        /// </summary>
        /// <param name="value">
        /// The value to find the successor of.
        /// </param>
        /// <returns>
        /// The successor of <paramref name="value"/> in this subtree if
        /// currently stored; otherwise, <see langword="null"/>.
        /// </returns>
        int? Successor(int value);
    }
}
