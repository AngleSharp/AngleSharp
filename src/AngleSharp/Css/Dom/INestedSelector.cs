namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// Indicates that the given selector is actually a nested
    /// selected "&amp;".
    /// </summary>
    public interface INestedSelector : ISelector
    {
        /// <summary>
        /// Gets or sets the parent selector.
        /// </summary>
        ISelector ParentSelector { get; set; }
    }
}