namespace AngleSharp.Css
{
    using System.Collections.Generic;
    using System.Linq;

    public class CssParseTree : CssNode
    {
        readonly List<CssNode> _nodes;

        public CssParseTree(TextRange range, IEnumerable<CssNode> nodes)
            : base(range)
        {
            _nodes = new List<CssNode>(nodes);
        }

        public IEnumerable<CssNode> Nodes
        {
            get { return _nodes.AsEnumerable(); }
        }
    }
}
