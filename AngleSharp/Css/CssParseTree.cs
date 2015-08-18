namespace AngleSharp.Css
{
    using AngleSharp.Css.Tree;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the ParseTree for a CSS file.
    /// </summary>
    public class CssParseTree : CssNode
    {
        /// <summary>
        /// Creates a new ParseTree.
        /// </summary>
        /// <param name="nodes">Nodes to be included.</param>
        /// <param name="range">The covered range in the source.</param>
        public CssParseTree(IEnumerable<CssNode> nodes, TextRange range)
            : base(range)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// Gets the contained nodes.
        /// </summary>
        public IEnumerable<CssNode> Nodes
        {
            get;
            private set;
        }
    }
}
