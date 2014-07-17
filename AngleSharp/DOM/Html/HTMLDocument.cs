namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Parser;
    using AngleSharp.Parser.Html;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an HTML document.
    /// </summary>
    sealed class HTMLDocument : Document, IHtmlDocument
    {
        #region ctor

        /// <summary>
        /// Creates a new Html document.
        /// </summary>
        internal HTMLDocument()
        {
            _contentType = MimeTypes.Xml;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens a document stream for writing.
        /// </summary>
        [DomName("open")]
        public void Open()
        {
            //TODO
        }

        /// <summary>
        /// Finishes writing to a document.
        /// </summary>
        [DomName("close")]
        public void Close()
        {
            //TODO
        }

        /// <summary>
        /// Writes text to a document.
        /// </summary>
        /// <param name="content">The text to be written on the document.</param>
        [DomName("write")]
        public void Write(String content)
        {
            //TODO
        }

        /// <summary>
        /// Writes a line of text to a document.
        /// </summary>
        /// <param name="content">The text to be written on the document.</param>
        [DomName("writeln")]
        public void WriteLn(String content)
        {
            Write(content + Specification.LineFeed);
        }

        #endregion

        #region Helpers

        #endregion
    }
}
