namespace AngleSharp.Css.Tree
{
    using System;

    /// <summary>
    /// Represents some trivia in the CSS AST.
    /// </summary>
    public class CssTriviaNode : CssNode
    {
        /// <summary>
        /// Creates a new trivia for the CSS AST.
        /// </summary>
        /// <param name="content">The contained content.</param>
        /// <param name="range">The covered range in the source.</param>
        public CssTriviaNode(String content, TextRange range)
            : base(range)
        {
            Content = content;
        }

        /// <summary>
        /// Gets the associated trivia content.
        /// </summary>
        public String Content 
        { 
            get; 
            private set; 
        }
    }
}
