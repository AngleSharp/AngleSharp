namespace AngleSharp.Css
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the ParseTree for a CSS file.
    /// </summary>
    public class CssParseTree : CssNode
    {
        readonly List<CssNode> _nodes;

        /// <summary>
        /// Creates a new ParseTree.
        /// </summary>
        /// <param name="range">The covered range in the source.</param>
        /// <param name="nodes">Nodes to be included.</param>
        public CssParseTree(TextRange range, IEnumerable<CssNode> nodes)
            : base(range)
        {
            _nodes = new List<CssNode>(nodes);
        }

        /// <summary>
        /// Gets the contained nodes.
        /// </summary>
        public IEnumerable<CssNode> Nodes
        {
            get { return _nodes.AsEnumerable(); }
        }
    }
}
