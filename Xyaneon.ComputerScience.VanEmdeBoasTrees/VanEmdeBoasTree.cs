using System;
using System.Collections.Generic;

namespace Xyaneon.ComputerScience.VanEmdeBoasTrees
{
    /// <summary>
    /// Represents a van Emde Boas tree.
    /// </summary>
    /// <seealso cref="IVanEmdeBoasTree"/>
    /// <seealso cref="VanEmdeBoasTreeNode"/>
    public class VanEmdeBoasTree : IVanEmdeBoasTree
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VanEmdeBoasTree"/> class
        /// with the provided <paramref name="universe"/> size.
        /// </summary>
        /// <param name="universe">
        /// The size of the universe for this tree.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="universe"/> is smaller than two.
        /// -or-
        /// <paramref name="universe"/> is not a power of two.
        /// </exception>
        /// <remarks>
        /// Calling this constructor will create an empty van Emde Boas subtree recursively.
        /// </remarks>
        public VanEmdeBoasTree(int universe)
        {
            VanEmdeBoasTreeNode root;

            try
            {
                root = new VanEmdeBoasTreeNode(universe);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(nameof(universe), universe, ex.Message);
            }

            Root = root;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VanEmdeBoasTree"/> class
        /// with the provided <paramref name="universe"/> size and <paramref name="values"/>.
        /// </summary>
        /// <param name="universe">
        /// The size of the universe for this tree.
        /// </param>
        /// <param name="values">
        /// The collection of values to initialize the tree with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="values"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="universe"/> is smaller than two.
        /// -or-
        /// <paramref name="universe"/> is not a power of two.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the elements of <paramref name="values"/> lies outside the universe.
        /// </exception>
        /// <remarks>
        /// Calling this constructor will create an van Emde Boas subtree recursively,
        /// which will contain the supplied values.
        /// </remarks>
        public VanEmdeBoasTree(int universe, IEnumerable<int> values) : this(universe)
        {
            if (values == null)
            {
                throw new ArgumentNullException(
                    nameof(values),
                    "The collection of values to initialize the van Emde Boas tree with cannot be null.");
            }

            foreach (int value in values)
            {
                try
                {
                    Root.Insert(value);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentException(ex.Message, nameof(values), ex);
                }
            }
        }

        #region IVanEmdeBoasTree implementation

        #region Properties

        /// <summary>
        /// Gets the maximum value stored in this tree.
        /// </summary>
        public int? Maximum { get => Root.Maximum; }

        /// <summary>
        /// Gets the minimum value stored in this tree.
        /// </summary>
        public int? Minimum { get => Root.Minimum; }

        /// <summary>
        /// Gets the size of the universe for this node.
        /// </summary>
        public int Universe { get => Root.Universe; }

        #endregion // End properties region.

        #region Methods

        /// <summary>
        /// Deletes <paramref name="value"/> from this subtree.
        /// </summary>
        /// <param name="value">
        /// The value to delete.
        /// </param>
        public void Delete(int value)
        {
            Root.Delete(value);
        }

        /// <summary>
        /// Inserts <paramref name="value"/> into this subtree.
        /// </summary>
        /// <param name="value">
        /// The value to insert.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is less than zero.
        /// -or-
        /// <paramref name="value"/> is equal to or greater than the
        /// value of this object's <see cref="Universe"/> property.
        /// </exception>
        public void Insert(int value)
        {
            try
            {
                Root.Insert(value);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, ex.Message);
            }
        }

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
        public bool Member(int value)
        {
            return Root.Member(value);
        }

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
        public int? Predecessor(int value)
        {
            return Root.Predecessor(value);
        }

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
        public int? Successor(int value)
        {
            return Root.Successor(value);
        }

        #endregion // End methods region.

        #endregion // End IVanEmdeBoasTree implementation region.

        #region Properties

        /// <summary>
        /// Gets the root of this van Emde Boas tree.
        /// </summary>
        public IVanEmdeBoasTreeNode Root { get; }

        #endregion // End properties region.
    }
}
