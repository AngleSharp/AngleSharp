namespace AngleSharp
{
    using System;
    using AngleSharp.Dom;

    /// <summary>
    /// Basic interface for HTML node serialization.
    /// </summary>
    public interface IMarkupFormatter
    {
        /// <summary>
        /// Formats the given text.
        /// </summary>
        /// <param name="text">The text to sanatize.</param>
        /// <returns>The formatted text.</returns>
        String Text(String text);

        /// <summary>
        /// Formats the given comment.
        /// </summary>
        /// <param name="comment">The comment to stringify.</param>
        /// <returns>The formatted comment.</returns>
        String Comment(IComment comment);

        /// <summary>
        /// Formats the given processing instruction using the target and the
        /// data.
        /// </summary>
        /// <param name="processing">
        /// The processing instruction to stringify.
        /// </param>
        /// <returns>The formatted processing instruction.</returns>
        String Processing(IProcessingInstruction processing);

        /// <summary>
        /// Formats the given doctype using the name, public and system
        /// identifiers.
        /// </summary>
        /// <param name="doctype">The document type to stringify.</param>
        /// <returns>The formatted doctype.</returns>
        String Doctype(IDocumentType doctype);

        /// <summary>
        /// Formats opening a tag with the given name.
        /// </summary>
        /// <param name="element">The element to open.</param>
        /// <param name="selfClosing">
        /// Is the element actually self-closing?
        /// </param>
        /// <returns>The formatted opening tag.</returns>
        String OpenTag(IElement element, Boolean selfClosing);

        /// <summary>
        /// Formats closing a tag with the given name.
        /// </summary>
        /// <param name="element">The element to close.</param>
        /// <param name="selfClosing">
        /// Is the element actually self-closing?
        /// </param>
        /// <returns>The formatted closing tag.</returns>
        String CloseTag(IElement element, Boolean selfClosing);

        /// <summary>
        /// Formats an attribute specified by the argument.
        /// </summary>
        /// <param name="attribute">The attribute to serialize.</param>
        /// <returns>The formatted attribute.</returns>
        String Attribute(IAttr attribute);
    }
}
