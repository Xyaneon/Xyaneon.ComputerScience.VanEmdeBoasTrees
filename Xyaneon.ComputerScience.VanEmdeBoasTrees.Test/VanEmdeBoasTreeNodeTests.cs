using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Xyaneon.ComputerScience.VanEmdeBoasTrees.Test
{
    /// <summary>
    /// Provides unit testing methods for the <see cref="VanEmdeBoasTreeNode"/> class.
    /// </summary>
    [TestClass]
    public class VanEmdeBoasTreeNodeTests
    {
        /// <summary>
        /// Tests the <see cref="VanEmdeBoasTreeNode"/> class against a
        /// test case from the CLRS book in Figure 20.6 on page 548, with
        /// the goal of ensuring that the
        /// <see cref="VanEmdeBoasTreeNode.Member(int)"/> method works.
        /// </summary>
        [TestMethod]
        public void VanEmdeBoasTreeNode_CLRS_Book_MembershipTest()
        {
            // Arrange.
            IReadOnlyList<int> values = TestData.Get_CLRS_Figure20_6_Values();
            const int universe = 16;
            int expectedMinimum = values.Min();
            int expectedMaximum = values.Max();
            IVanEmdeBoasTree tree;

            // Act.
            tree = ConstructVanEmdeBoasTree(universe, values);

            // Assert.
            for (int i = 0; i <= universe; i++)
            {
                if (values.Contains(i))
                {
                    Assert.IsTrue(
                        tree.Member(i),
                        $"The van Emde Boas tree claims not to contain a value it should ({i}).");
                }
                else
                {
                    Assert.IsFalse(
                        tree.Member(i),
                        $"The van Emde Boas tree claims to contain a value it should not ({i}).");
                }
            }
        }

        /// <summary>
        /// Tests the <see cref="VanEmdeBoasTreeNode"/> class against a
        /// test case from the CLRS book in Figure 20.6 on page 548, with
        /// the goal of ensuring that the
        /// <see cref="VanEmdeBoasTreeNode.Predecessor(int)"/> method works.
        /// </summary>
        [TestMethod]
        public void VanEmdeBoasTreeNode_CLRS_Book_PredecessorTest()
        {
            // Arrange.
            IReadOnlyList<int> values = TestData.Get_CLRS_Figure20_6_Values();
            const int universe = 16;
            int expectedMinimum = values.Min();
            int expectedMaximum = values.Max();
            IVanEmdeBoasTree tree;

            // Act.
            tree = ConstructVanEmdeBoasTree(universe, values);

            // Assert.
            int? predecessorFound = tree.Predecessor(expectedMinimum);
            int expectedPredecessor;
            Assert.IsNull(
                predecessorFound,
                $"The van Emde Boas tree claims to have found a predecessor to the smallest value ({predecessorFound}).");
            for (int i = 1; i < values.Count; i++)
            {
                predecessorFound = tree.Predecessor(values[i]);

                Assert.IsNotNull(
                    predecessorFound,
                    $"The van Emde Boas tree could not find a predecessor to value {values[i]}.");

                expectedPredecessor = values[i - 1];
                Assert.AreEqual(
                    expectedPredecessor,
                    predecessorFound,
                    $"The predecessor the van Emde Boas tree found for value {values[i]} was {predecessorFound}, not {expectedPredecessor}.");
            }
        }

        /// <summary>
        /// Tests the <see cref="VanEmdeBoasTreeNode"/> class against a
        /// test case from the CLRS book in Figure 20.6 on page 548, with
        /// the goal of ensuring that the
        /// <see cref="VanEmdeBoasTreeNode.Successor(int)"/> method works.
        /// </summary>
        [TestMethod]
        public void VanEmdeBoasTreeNode_CLRS_Book_SuccessorTest()
        {
            // Arrange.
            IReadOnlyList<int> values = TestData.Get_CLRS_Figure20_6_Values();
            const int universe = 16;
            int expectedMinimum = values.Min();
            int expectedMaximum = values.Max();
            IVanEmdeBoasTree tree;

            // Act.
            tree = ConstructVanEmdeBoasTree(universe, values);

            // Assert.
            int? successorFound = tree.Successor(expectedMaximum);
            int expectedSuccessor;
            Assert.IsNull(
                successorFound,
                $"The van Emde Boas tree claims to have found a successor to the largest value ({successorFound}).");
            for (int i = 0; i < values.Count - 1; i++)
            {
                successorFound = tree.Successor(values[i]);

                Assert.IsNotNull(
                    successorFound,
                    $"The van Emde Boas tree could not find a successor to value {values[i]}.");

                expectedSuccessor = values[i + 1];
                Assert.AreEqual(
                    expectedSuccessor,
                    successorFound,
                    $"The successor the van Emde Boas tree found for value {values[i]} was {successorFound}, not {expectedSuccessor}.");
            }
        }

        /// <summary>
        /// Tests the <see cref="VanEmdeBoasTreeNode"/> class against a
        /// test case from the CLRS book in Figure 20.6 on page 548, with
        /// the goal of ensuring the tree has the expected structure.
        /// </summary>
        [TestMethod]
        public void VanEmdeBoasTreeNode_CLRS_Book_TreeStructureTest()
        {
            // Arrange.
            IReadOnlyList<int> values = TestData.Get_CLRS_Figure20_6_Values();
            const int universe = 16;
            int expectedMinimum = values.Min();
            int expectedMaximum = values.Max();
            IVanEmdeBoasTreeNode tree;

            // Act.
            tree = ConstructVanEmdeBoasTree(universe, values);

            // Assert.

            // Check the root node.
            Utility.CheckNode(tree, "The van Emde Boas tree root", universe, expectedMinimum, expectedMaximum, false, 4);

            // Check depth 1.
            Utility.CheckNode(tree.Summary, "vEB(4) node 1", 4, 0, 3, false, 2);
            Utility.CheckNode(tree.Cluster[0], "vEB(4) node 2", 4, 3, 3, false, 2);
            Utility.CheckNode(tree.Cluster[1], "vEB(4) node 3", 4, 0, 3, false, 2);
            Utility.CheckNode(tree.Cluster[2], "vEB(4) node 4", 4, null, null, false, 2);
            Utility.CheckNode(tree.Cluster[3], "vEB(4) node 5", 4, 2, 3, false, 2);

            // Check depth 2.
            Utility.CheckNode(tree.Summary.Summary, "vEB(2) node 1", 2, 0, 1, true, 0);
            Utility.CheckNode(tree.Summary.Cluster[0], "vEB(2) node 2", 2, 1, 1, true, 0);
            Utility.CheckNode(tree.Summary.Cluster[1], "vEB(2) node 3", 2, 1, 1, true, 0);

            Utility.CheckNode(tree.Cluster[0].Summary, "vEB(2) node 4", 2, null, null, true, 0);
            Utility.CheckNode(tree.Cluster[0].Cluster[0], "vEB(2) node 5", 2, null, null, true, 0);
            Utility.CheckNode(tree.Cluster[0].Cluster[1], "vEB(2) node 6", 2, null, null, true, 0);

            Utility.CheckNode(tree.Cluster[1].Summary, "vEB(2) node 7", 2, 0, 1, true, 0);
            Utility.CheckNode(tree.Cluster[1].Cluster[0], "vEB(2) node 8", 2, 1, 1, true, 0);
            Utility.CheckNode(tree.Cluster[1].Cluster[1], "vEB(2) node 9", 2, 1, 1, true, 0);

            Utility.CheckNode(tree.Cluster[2].Summary, "vEB(2) node 10", 2, null, null, true, 0);
            Utility.CheckNode(tree.Cluster[2].Cluster[0], "vEB(2) node 11", 2, null, null, true, 0);
            Utility.CheckNode(tree.Cluster[2].Cluster[1], "vEB(2) node 12", 2, null, null, true, 0);

            Utility.CheckNode(tree.Cluster[3].Summary, "vEB(2) node 13", 2, 1, 1, true, 0);
            Utility.CheckNode(tree.Cluster[3].Cluster[0], "vEB(2) node 14", 2, null, null, true, 0);
            Utility.CheckNode(tree.Cluster[3].Cluster[1], "vEB(2) node 15", 2, 1, 1, true, 0);
        }

        #region Helper methods

        private static VanEmdeBoasTreeNode ConstructVanEmdeBoasTree(int universe, IEnumerable<int> values)
        {
            var tree = new VanEmdeBoasTreeNode(universe);
            foreach (int value in values)
            {
                tree.Insert(value);
            }
            return tree;
        }

        #endregion // End helper methods region.
    }
}
