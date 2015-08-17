namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a single node in the CSS AST.
    /// </summary>
    public class CssNode
    {
        /// <summary>
        /// Creates a new Node for the CSS AST.
        /// </summary>
        /// <param name="range">The covered range in the source.</param>
        public CssNode(TextRange range)
        {
            Range = range;
        }

        /// <summary>
        /// Gets the covered range within the source file.
        /// </summary>
        public TextRange Range
        {
            get;
            private set;
        }
    }
}
