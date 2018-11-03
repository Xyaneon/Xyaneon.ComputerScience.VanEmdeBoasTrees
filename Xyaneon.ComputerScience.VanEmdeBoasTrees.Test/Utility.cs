using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Xyaneon.ComputerScience.VanEmdeBoasTrees.Test
{
    /// <summary>
    /// Provides utility methods to the rest of this assembly.
    /// </summary>
    internal static class Utility
    {
        /// <summary>
        /// Checks whether the provided van Emde Boas tree node meets
        /// the specified criteria, and throws an exception if not.
        /// </summary>
        /// <param name="node">
        /// The van Emde Boas tree node to check.
        /// </param>
        /// <param name="nodeName">
        /// A displayable name for the node.
        /// </param>
        /// <param name="expectedUniverse">
        /// The expected value of the <paramref name="node"/>'s
        /// <see cref="IVanEmdeBoasTree.Universe"/> property.
        /// </param>
        /// <param name="expectedMinimum">
        /// The expected value of the <paramref name="node"/>'s
        /// <see cref="IVanEmdeBoasTree.Minimum"/> property.
        /// </param>
        /// <param name="expectedMaximum">
        /// The expected value of the <paramref name="node"/>'s
        /// <see cref="IVanEmdeBoasTree.Maximum"/> property.
        /// </param>
        /// <param name="shouldSummaryBeNull">
        /// Whether the <paramref name="node"/>'s
        /// <see cref="IVanEmdeBoasTreeNode.Summary"/> property
        /// is supposed to be <see langword="null"/>.
        /// </param>
        /// <param name="expectedClusterSize">
        /// The expected size of the <paramref name="node"/>'s
        /// <see cref="IVanEmdeBoasTreeNode.Cluster"/> property's
        /// stored collection.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="node"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="AssertFailedException">
        /// <paramref name="node"/> failed one of the checks.
        /// </exception>
        public static void CheckNode(
            IVanEmdeBoasTreeNode node,
            string nodeName,
            int expectedUniverse,
            int? expectedMinimum,
            int? expectedMaximum,
            bool shouldSummaryBeNull,
            int expectedClusterSize)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            Assert.AreEqual(
                expectedUniverse,
                node.Universe,
                $"{nodeName} claims a universe size of {node.Universe} instead of {expectedUniverse}.");

            Assert.AreEqual(
                expectedMinimum,
                node.Minimum,
                $"{nodeName} claims a minimum of {node.Minimum} instead of {expectedMinimum}.");

            Assert.AreEqual(
                expectedMaximum,
                node.Maximum,
                $"{nodeName} claims a maximum of {node.Maximum} instead of {expectedMaximum}.");

            if (shouldSummaryBeNull)
            {
                Assert.IsNull(
                    node.Summary,
                    $"{nodeName} has a non-null summary pointer.");
            }
            else
            {
                Assert.IsNotNull(
                    node.Summary,
                    $"{nodeName} has a null summary pointer.");
            }

            if (expectedClusterSize == 0)
            {
                Assert.IsNull(
                node.Cluster,
                $"{nodeName} has a non-null cluster list pointer.");
            }
            else
            {
                Assert.IsNotNull(
                    node.Cluster,
                    $"{nodeName} has a null cluster list pointer.");

                int actualClusterSize = node.Cluster.Count;
                Assert.AreEqual(
                    expectedClusterSize,
                    actualClusterSize,
                    $"{nodeName} has {actualClusterSize} element(s) instead of {expectedClusterSize}.");
            }
        }
    }
}
