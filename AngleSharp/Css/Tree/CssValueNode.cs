namespace AngleSharp.Css.Tree
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an value part in the CSS AST.
    /// </summary>
    public class CssValueNode : CssNode
    {
        /// <summary>
        /// Creates a new value node for the CSS AST.
        /// </summary>
        /// <param name="text">The contained text.</param>
        /// <param name="range">The covered range in the source.</param>
        public CssValueNode(IEnumerable<CssNode> nodes, Boolean important, TextRange range)
            : base(range)
        {
            Nodes = nodes;
            IsImportant = important;
        }

        /// <summary>
        /// Gets the associated value nodes.
        /// </summary>
        public IEnumerable<CssNode> Nodes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets if the important flag is set.
        /// </summary>
        public Boolean IsImportant 
        { 
            get; 
            private set; 
        }
    }
}
