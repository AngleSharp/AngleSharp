namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Represents the CSS cursor property.
    /// </summary>
    public interface ICssCursorProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected system cursor, if any.
        /// </summary>
        SystemCursor Cursor { get; }
    }
}
