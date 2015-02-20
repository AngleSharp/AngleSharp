namespace AngleSharp
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

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
        /// Formats the given processing instruction using the target and the data.
        /// </summary>
        /// <param name="processing">The processing instruction to stringify.</param>
        /// <returns>The formatted processing instruction.</returns>
        String Processing(IProcessingInstruction processing);

        /// <summary>
        /// Formats the given doctype using the name, public and system identifiers.
        /// </summary>
        /// <param name="doctype">The document type to stringify.</param>
        /// <returns>The formatted doctype.</returns>
        String Doctype(IDocumentType doctype);

        /// <summary>
        /// Formats the provided attribute containing the given properties.
        /// </summary>
        /// <param name="attribute">The attribute to stringify.</param>
        /// <returns>The formatted attribute.</returns>
        String Attribute(IAttr attribute);

        /// <summary>
        /// Formats opening a tag with the given name.
        /// </summary>
        /// <param name="tagName">The name of the element.</param>
        /// <param name="attributes">The attributes of the element.</param>
        /// <param name="selfClosing">Is the element actually self-closing?</param>
        /// <returns>The formatted opening tag.</returns>
        String OpenTag(String tagName, IEnumerable<String> attributes, Boolean selfClosing);

        /// <summary>
        /// Formats closing a tag with the given name.
        /// </summary>
        /// <param name="tagName">The name of the element.</param>
        /// <param name="selfClosing">Is the element actually self-closing?</param>
        /// <returns>Th eformatted closing tag.</returns>
        String CloseTag(String tagName, Boolean selfClosing);
    }
}
