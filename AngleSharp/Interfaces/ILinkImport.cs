namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Implemented by elements that may expose imports.
    /// </summary>
    [DomName("LinkImport")]
    [DomNoInterfaceObject]
    public interface ILinkImport
    {
        /// <summary>
        /// Gets the Document object associated with the given element, or null
        /// if there is none.
        /// </summary>
        [DomName("import")]
        IDocument Import { get; }
    }
}
