namespace AngleSharp.Css.Tree
{
    using System;

    /// <summary>
    /// Represents some identifier in the CSS AST.
    /// </summary>
    public class CssIdentifierNode : CssNode
    {
        /// <summary>
        /// Creates a new identifier for the CSS AST.
        /// </summary>
        /// <param name="name">The transported name.</param>
        /// <param name="range">The covered range in the source.</param>
        public CssIdentifierNode(String name, TextRange range)
            : base(range)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the associated name.
        /// </summary>
        public String Name 
        { 
            get; 
            private set; 
        }
    }
}
