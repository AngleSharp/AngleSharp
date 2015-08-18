namespace AngleSharp.Css.Tree
{
    using System;

    /// <summary>
    /// Represents some declaration in the CSS AST.
    /// </summary>
    public class CssDeclarationNode : CssNode
    {
        /// <summary>
        /// Creates a new declaration for the CSS AST.
        /// </summary>
        /// <param name="name">The name node of the declaration.</param>
        /// <param name="value">The value node of the declaration.</param>
        /// <param name="range">The covered range in the source.</param>
        public CssDeclarationNode(CssNode name, CssNode value, TextRange range)
            : base(range)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets the associated name.
        /// </summary>
        public CssNode Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the associated value node.
        /// </summary>
        public CssNode Value 
        { 
            get; 
            private set; 
        }
    }
}
