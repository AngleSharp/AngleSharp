namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Extensions;
    using AngleSharp.Network;

    /// <summary>
    /// Represents a document node that contains only HTML nodes.
    /// </summary>
    sealed class HtmlDocument : Document, IHtmlDocument
    {
        internal HtmlDocument(IBrowsingContext context, TextSource source)
            : base(context, source)
        {
            ContentType = MimeTypes.Html;
        }

        internal HtmlDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }
    }
}
