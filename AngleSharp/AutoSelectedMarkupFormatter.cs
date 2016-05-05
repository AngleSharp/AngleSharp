namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.XHtml;
    using AngleSharp.Xml;
    using System;
    
    /// <summary>
    /// AutoSelectedMarkupFormatter class to select the proper MarkupFormatter
    /// implementation depending on the used document type.
    /// </summary>
    public sealed class AutoSelectedMarkupFormatter : IMarkupFormatter
    {
        #region Fields

        IMarkupFormatter childFormatter = null;
        IDocumentType _docType;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the auto selected markup formatter.
        /// </summary>
        /// <param name="docType">
        /// Optional DocumentType to hint the implementation to use.
        /// </param>
        public AutoSelectedMarkupFormatter(IDocumentType docType = null)
        {
            _docType = docType;
        }

        #endregion

        #region Properties

        IMarkupFormatter ChildFormatter
        {
            get
            {
                if (childFormatter == null && _docType != null)
                {
                    if (_docType.PublicIdentifier.Contains("XML"))
                    {
                        childFormatter = XmlMarkupFormatter.Instance;
                    }
                    else if (_docType.PublicIdentifier.Contains("XHTML"))
                    {
                        childFormatter = XhtmlMarkupFormatter.Instance;
                    }
                }

                return childFormatter ?? HtmlMarkupFormatter.Instance;
            }
            set
            {
                childFormatter = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Formats an attribute specified by the argument.
        /// </summary>
        /// <param name="attribute">The attribute to serialize.</param>
        /// <returns>The formatted attribute.</returns>
        public String Attribute(IAttr attribute)
        {
            return ChildFormatter.Attribute(attribute);
        }

        /// <summary>
        /// Formats opening a tag with the given name.
        /// </summary>
        /// <param name="element">The element to open.</param>
        /// <param name="selfClosing">
        /// Is the element actually self-closing?
        /// </param>
        /// <returns>The formatted opening tag.</returns>
        public String OpenTag(IElement element, Boolean selfClosing)
        {
            Confirm(element.Owner.Doctype);
            return ChildFormatter.OpenTag(element, selfClosing);
        }

        /// <summary>
        /// Formats closing a tag with the given name.
        /// </summary>
        /// <param name="element">The element to close.</param>
        /// <param name="selfClosing">
        /// Is the element actually self-closing?
        /// </param>
        /// <returns>The formatted closing tag.</returns>
        public String CloseTag(IElement element, Boolean selfClosing)
        {
            Confirm(element.Owner.Doctype);
            return ChildFormatter.CloseTag(element, selfClosing);
        }

        /// <summary>
        /// Formats the given comment.
        /// </summary>
        /// <param name="comment">The comment to stringify.</param>
        /// <returns>The formatted comment.</returns>
        public String Comment(IComment comment)
        {
            Confirm(comment.Owner.Doctype);
            return ChildFormatter.Comment(comment);
        }

        /// <summary>
        /// Formats the given doctype using the name, public and system
        /// identifiers.
        /// </summary>
        /// <param name="doctype">The document type to stringify.</param>
        /// <returns>The formatted doctype.</returns>
        public String Doctype(IDocumentType doctype)
        {
            Confirm(doctype);
            return ChildFormatter.Doctype(doctype);
        }

        /// <summary>
        /// Formats the given processing instruction using the target and the
        /// data.
        /// </summary>
        /// <param name="processing">
        /// The processing instruction to stringify.
        /// </param>
        /// <returns>The formatted processing instruction.</returns>
        public String Processing(IProcessingInstruction processing)
        {
            Confirm(processing.Owner.Doctype);
            return ChildFormatter.Processing(processing);
        }

        /// <summary>
        /// Formats the given text.
        /// </summary>
        /// <param name="text">The text to sanatize.</param>
        /// <returns>The formatted text.</returns>
        public String Text(String text)
        {
            return ChildFormatter.Text(text);
        }

        #endregion

        #region Helpers

        void Confirm(IDocumentType docType)
        {
            if (_docType == null)
            {
                _docType = docType;
            }
        }

        #endregion
    }
}
