namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Represents an element factory.
    /// </summary>
    /// <typeparam name="TDocument">The type of root document.</typeparam>
    /// <typeparam name="TElement">The common type of elements to create.</typeparam>
    public interface IElementFactory<TDocument, TElement>
        where TElement : IElement
        where TDocument : IDocument
    {
        /// <summary>
        /// Creates a new element with the given local name and optional prefix.
        /// </summary>
        /// <param name="document">The owner of the element.</param>
        /// <param name="localName">The local name of the element.</param>
        /// <param name="prefix">The optional prefix of the element.</param>
        /// <param name="flags">The optional flags for the node.</param>
        /// <returns>The created document's child element.</returns>
        TElement Create(TDocument document, String localName, String prefix = null, NodeFlags flags = NodeFlags.None);
    }
}
