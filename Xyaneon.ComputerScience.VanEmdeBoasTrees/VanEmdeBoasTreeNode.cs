using System;
using System.Collections.Generic;

namespace Xyaneon.ComputerScience.VanEmdeBoasTrees
{
    /// <summary>
    /// Represents a node in a van Emde Boas tree.
    /// </summary>
    /// <seealso cref="IVanEmdeBoasTreeNode"/>
    public class VanEmdeBoasTreeNode : IVanEmdeBoasTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VanEmdeBoasTreeNode"/> class
        /// with the provided <paramref name="universe"/> size.
        /// </summary>
        /// <param name="universe">
        /// The size of the universe for this node.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="universe"/> is smaller than two.
        /// -or-
        /// <paramref name="universe"/> is not a power of two.
        /// </exception>
        /// <remarks>
        /// Calling this constructor will create an empty van Emde Boas subtree recursively.
        /// </remarks>
        public VanEmdeBoasTreeNode(int universe)
        {
            if (universe < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(universe), universe, "The universe cannot be smaller than two.");
            }

            if (!Utility.IsPowerOfTwo(universe))
            {
                throw new ArgumentOutOfRangeException(nameof(universe), universe, "The universe must be a power of two.");
            }

            Universe = universe;
            Maximum = null;
            Minimum = null;

            if (universe == 2)
            {
                // Base case: the node has no cluster or summary.
                Cluster = null;
                Summary = null;
            }
            else
            {
                int universeUpperSquareRoot = (int)Utility.UpperSquareRoot(universe);
                int universeLowerSquareRoot = (int)Utility.LowerSquareRoot(universe);

                Summary = new VanEmdeBoasTreeNode(universeUpperSquareRoot);

                var clusterList = new List<VanEmdeBoasTreeNode>(universeUpperSquareRoot);
                for (int i = 0; i < universeUpperSquareRoot; i++)
                {
                    clusterList.Add(new VanEmdeBoasTreeNode(universeLowerSquareRoot));
                }
                Cluster = clusterList.AsReadOnly();
            }
        }

        #region IVanEmdeBoasTreeNode implementation

        #region IVanEmdeBoasTreeNode Properties

        /// <summary>
        /// Gets this node's cluster.
        /// </summary>
        public IReadOnlyList<IVanEmdeBoasTreeNode> Cluster { get; }

        /// <summary>
        /// Gets the maximum value stored under this node.
        /// </summary>
        public int? Maximum { get; private set; }

        /// <summary>
        /// Gets the minimum value stored under this node.
        /// </summary>
        public int? Minimum { get; private set; }

        /// <summary>
        /// Gets this node's summary.
        /// </summary>
        public IVanEmdeBoasTreeNode Summary { get; }

        /// <summary>
        /// Gets the size of the universe for this node.
        /// </summary>
        public int Universe { get; }

        #endregion // End IVanEmdeBoasTreeNode properties region.

        #region IVanEmdeBoasTreeNode Methods

        /// <summary>
        /// Deletes <paramref name="x"/> from this subtree.
        /// </summary>
        /// <param name="x">
        /// The value to delete.
        /// </param>
        /// <remarks>
        /// This method is based upon the vEB-Tree-Delete(V, x)
        /// algorithm pseudocode from the CLRS book on page 554.
        /// </remarks>
        public void Delete(int x)
        {
            if (x < 0 || x >= Universe)
            {
                // Simply return if the value to delete is outside of
                // the universe, since it can never be in this tree.
                return;
            }

            if (Minimum == Maximum)
            {
                Minimum = null;
                Maximum = null;
            }
            else if (Universe == 2)
            {
                Minimum = x == 0 ? 1 : 0;
                Maximum = Minimum;
            }
            else
            {
                if (x == Minimum)
                {
                    int? firstCluster = Summary.Minimum;
                    x = Index(firstCluster.Value, Cluster[firstCluster.Value].Minimum.Value);
                    Minimum = x;
                }

                int highX = High(x);

                Cluster[highX].Delete(Low(x));

                if (!Cluster[highX].Minimum.HasValue)
                {
                    Summary.Delete(highX);
                    if (x == Maximum)
                    {
                        int? summaryMax = Summary.Maximum;
                        if (!summaryMax.HasValue)
                        {
                            Maximum = Minimum;
                        }
                        else
                        {
                            Maximum = Index(summaryMax.Value, Cluster[summaryMax.Value].Maximum.Value);
                        }
                    }
                }
                else if (x == Maximum)
                {
                    Maximum = Index(highX, Cluster[highX].Maximum.Value);
                }
            }
        }

        /// <summary>
        /// Inserts <paramref name="x"/> into this subtree.
        /// </summary>
        /// <param name="x">
        /// The value to insert.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="x"/> is less than zero.
        /// -or-
        /// <paramref name="x"/> is equal to or greater than the
        /// value of this object's <see cref="Universe"/> property.
        /// </exception>
        /// <remarks>
        /// This method is based upon the vEB-Tree-Insert(V, x)
        /// algorithm pseudocode from the CLRS book on page 553.
        /// </remarks>
        public void Insert(int x)
        {
            if (x < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(x), x, "The value to insert cannot be less than zero.");
            }

            if (x >= Universe)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(x), x, $"The value to insert cannot be equal to or greater than the size of this tree's universe ({Universe}).");
            }

            if (!Minimum.HasValue)
            {
                EmptyInsert(x);
            }
            else
            {
                if (x < Minimum)
                {
                    int swap = Minimum.Value;
                    Minimum = x;
                    x = swap;
                }

                if (Universe > 2)
                {
                    int highX = High(x);
                    int lowX = Low(x);

                    if (!Cluster[highX].Minimum.HasValue)
                    {
                        Summary.Insert(highX);
                        ((VanEmdeBoasTreeNode)Cluster[highX]).EmptyInsert(lowX);
                    }
                    else
                    {
                        Cluster[highX].Insert(lowX);
                    }
                }

                if (x > Maximum)
                {
                    Maximum = x;
                }
            }
        }

        /// <summary>
        /// Determines whether <paramref name="x"/> is contained
        /// in this subtree.
        /// </summary>
        /// <param name="x">
        /// The value to look for.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="x"/> is
        /// contained in this subtree; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// This method is based upon the vEB-Tree-Member(V, x)
        /// algorithm pseudocode from the CLRS book on page 550.
        /// </remarks>
        public bool Member(int x)
        {
            if (x < 0 || x >= Universe)
            {
                // Values less than zero, or equal to or greater than
                // the size of the universe, can never be members of the tree.
                return false;
            }

            if (x == Minimum || x == Maximum)
            {
                return true;
            }

            if (Universe == 2)
            {
                return false;
            }

            return Cluster[High(x)].Member(Low(x));
        }

        /// <summary>
        /// Determines the predecessor of <paramref name="x"/> in this
        /// subtree.
        /// </summary>
        /// <param name="x">
        /// The value to find the predecessor of.
        /// </param>
        /// <returns>
        /// The predecessor of <paramref name="x"/> in this subtree if
        /// currently stored; otherwise, <see langword="null"/>.
        /// </returns>
        /// <remarks>
        /// This method is based upon the vEB-Tree-predecessor(V, x)
        /// algorithm pseudocode from the CLRS book on page 552.
        /// </remarks>
        public int? Predecessor(int x)
        {
            if (Universe == 2)
            {
                if (x == 1 && Minimum == 0)
                {
                    return 0;
                }
                else
                {
                    return null;
                }
            }

            if (Maximum.HasValue && x > Maximum)
            {
                return Maximum;
            }

            int highX = High(x);
            int lowX = Low(x);
            int? minLow = Cluster[highX].Minimum;

            if (minLow.HasValue && lowX > minLow)
            {
                int? offset = Cluster[highX].Predecessor(lowX);
                return Index(highX, offset.Value);
            }

            int? predecessorCluster = Summary.Predecessor(highX);
            if (!predecessorCluster.HasValue)
            {
                if (Minimum.HasValue && x > Minimum)
                {
                    return Minimum;
                }
                return null;
            }
            else
            {
                int? offset = Cluster[predecessorCluster.Value].Maximum;
                return Index(predecessorCluster.Value, offset.Value);
            }
        }

        /// <summary>
        /// Determines the successor of <paramref name="x"/> in this
        /// subtree.
        /// </summary>
        /// <param name="x">
        /// The value to find the successor of.
        /// </param>
        /// <returns>
        /// The successor of <paramref name="x"/> in this subtree if
        /// currently stored; otherwise, <see langword="null"/>.
        /// </returns>
        /// <remarks>
        /// This method is based upon the vEB-Tree-Successor(V, x)
        /// algorithm pseudocode from the CLRS book on page 551.
        /// </remarks>
        public int? Successor(int x)
        {
            if (Universe == 2)
            {
                if (x == 0 && Maximum == 1)
                {
                    return 1;
                }
                else
                {
                    return null;
                }
            }

            if (Minimum.HasValue && x < Minimum)
            {
                return Minimum;
            }

            int highX = High(x);
            int lowX = Low(x);
            int? maxLow = Cluster[highX].Maximum;

            if (maxLow.HasValue && lowX < maxLow)
            {
                int? offset = Cluster[highX].Successor(lowX);
                return Index(highX, offset.Value);
            }

            int? successorCluster = Summary.Successor(highX);
            if (!successorCluster.HasValue)
            {
                return null;
            }
            else
            {
                int? offset = Cluster[successorCluster.Value].Minimum;
                return Index(successorCluster.Value, offset.Value);
            }
        }

        #endregion // End IVanEmdeBoasTreeNode methods region.

        #endregion // End IVanEmdeBoasTreeNode implementation region.

        #region Private methods

        /// <summary>
        /// Inserts an only element into this subtree.
        /// </summary>
        /// <param name="x">
        /// The value to insert.
        /// </param>
        /// <remarks>
        /// This method is based upon the vEB-Empty-Tree-Insert(V, x)
        /// algorithm pseudocode from the CLRS book on page 553.
        /// </remarks>
        private void EmptyInsert(int x)
        {
            Minimum = x;
            Maximum = x;
        }

        /// <summary>
        /// Computes the most significant (lg <see cref="Universe"/>) / 2
        /// bits of <paramref name="x"/> (e.g., the number of
        /// <paramref name="x"/>'s cluster).
        /// </summary>
        /// <param name="x">
        /// A value which could be stored in this tree.
        /// </param>
        /// <returns>
        /// The most significant (lg <see cref="Universe"/>) / 2
        /// bits of <paramref name="x"/> (e.g., the number of
        /// <paramref name="x"/>'s cluster).
        /// </returns>
        private int High(int x)
        {
            return (int)Math.Floor(x / Utility.LowerSquareRoot(Universe));
        }

        /// <summary>
        /// Constructs an element number from <paramref name="x"/> and
        /// <paramref name="y"/>.
        /// </summary>
        /// <param name="x">
        /// The most significant (lg <see cref="Universe"/>) / 2 bits of
        /// the element number.
        /// </param>
        /// <param name="y">
        /// The least significant (lg <see cref="Universe"/>) / 2 bits of
        /// the element number.
        /// </param>
        /// <returns>
        /// The element number constructed from <paramref name="x"/> and
        /// <paramref name="y"/>.
        /// </returns>
        private int Index(int x, int y)
        {
            return x * (int)Utility.LowerSquareRoot(Universe) + y;
        }

        /// <summary>
        /// Computes the least significant (lg <see cref="Universe"/>) / 2
        /// bits of <paramref name="x"/> (e.g., the position of
        /// <paramref name="x"/> within its cluster).
        /// </summary>
        /// <param name="x">
        /// A value which could be stored in this tree.
        /// </param>
        /// <returns>
        /// The least significant (lg <see cref="Universe"/>) / 2
        /// bits of <paramref name="x"/> (e.g., the position of
        /// <paramref name="x"/> within its cluster).
        /// </returns>
        private int Low(int x)
        {
            return x % (int)Utility.LowerSquareRoot(Universe);
        }

        #endregion // End private methods region.
    }
}
