using System.Collections.Generic;

namespace Xyaneon.ComputerScience.VanEmdeBoasTrees
{
    /// <summary>
    /// Exposes properties and methods on classes which represent
    /// a van Emde Boas tree node.
    /// </summary>
    /// <seealso cref="IVanEmdeBoasTree"/>
    public interface IVanEmdeBoasTreeNode : IVanEmdeBoasTree
    {
        /// <summary>
        /// Gets this node's cluster.
        /// </summary>
        IReadOnlyList<IVanEmdeBoasTreeNode> Cluster { get; }

        /// <summary>
        /// Gets this node's summary.
        /// </summary>
        IVanEmdeBoasTreeNode Summary { get; }
    }
}
