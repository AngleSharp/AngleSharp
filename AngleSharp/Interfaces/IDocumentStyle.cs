namespace AngleSharp.DOM
{
    /// <summary>
    /// Extends the document with further properties for styling.
    /// </summary>
    [DomName("DocumentStyle")]
    public interface IDocumentStyle
    {
        /// <summary>
        /// Gets a list of stylesheet objects for stylesheets explicitly linked into or embedded in a document.
        /// </summary>
        [DomName("styleSheets")]
        IStyleSheetList StyleSheets { get; } 
    }
}
