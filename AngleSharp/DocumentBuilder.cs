using System;
using System.Diagnostics;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using System.IO;
using AngleSharp.Html;
using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Css;
using AngleSharp.Css;
using AngleSharp.DOM.Xml;
using AngleSharp.Xml;

namespace AngleSharp
{
    /// <summary>
    /// A handy helper to construct various kinds of documents
    /// from a given source code, URL or stream.
    /// </summary>
    public sealed class DocumentBuilder
    {
        #region Members

        IParser parser;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new builder with the specified source.
        /// </summary>
        /// <param name="source">The code manager.</param>
        /// <param name="document">The document to fill.</param>
        DocumentBuilder(SourceManager source, HTMLDocument document)
        {
            parser = new HtmlParser(document, source);
            parser.ErrorOccurred += ParseErrorOccurred;
        }

        /// <summary>
        /// Creates a new builder with the specified source.
        /// </summary>
        /// <param name="source">The code manager.</param>
        /// <param name="document">The document to fill.</param>
        DocumentBuilder(SourceManager source, XMLDocument document)
        {
            parser = new XmlParser(document, source);
            parser.ErrorOccurred += ParseErrorOccurred;
        }

        /// <summary>
        /// Creates a new builder with the specified source.
        /// </summary>
        /// <param name="source">The code manager.</param>
        /// <param name="sheet">The document to fill.</param>
        DocumentBuilder(SourceManager source, CSSStyleSheet sheet)
        {
            parser = new CssParser(sheet, source);
            parser.ErrorOccurred += ParseErrorOccurred;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the result of an HTML parsing.
        /// </summary>
        public HTMLDocument HtmlResult
        {
            get { return ((HtmlParser)parser).Result; }
        }

        /// <summary>
        /// Gets the result of an XML parsing.
        /// </summary>
        public XMLDocument XmlResult
        {
            get { return ((XmlParser)parser).Result; }
        }

        /// <summary>
        /// Gets the result of a CSS parsing.
        /// </summary>
        public CSSStyleSheet CssResult
        {
            get { return ((CssParser)parser).Result; }
        }

        #endregion

        #region XML Construction

        /// <summary>
        /// Builds a new XMLDocument with the given source code string.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <returns>The constructed XML document.</returns>
        public static XMLDocument Xml(String sourceCode)
        {
            var source = new SourceManager(sourceCode);
            var db = new DocumentBuilder(source, new XMLDocument());
            return db.XmlResult;
        }

        /// <summary>
        /// Builds a new XMLDocument with the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <returns>The constructed XML document.</returns>
        public static XMLDocument Xml(Uri url)
        {
            var stream = Builder.Stream(url);
            var source = new SourceManager(stream);
            var db = new DocumentBuilder(source, new XMLDocument());
            return db.XmlResult;
        }

        /// <summary>
        /// Builds a new XMLDocument with the given network stream.
        /// </summary>
        /// <param name="networkStream">The stream of chars to use as source code.</param>
        /// <returns>The constructed XML document.</returns>
        public static XMLDocument Xml(Stream networkStream)
        {
            var source = new SourceManager(networkStream);
            var db = new DocumentBuilder(source, new XMLDocument());
            return db.XmlResult;
        }

        #endregion

        #region HTML Construction

        /// <summary>
        /// Builds a new HTMLDocument with the given source code string.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <returns>The constructed HTML document.</returns>
        public static HTMLDocument Html(String sourceCode)
        {
            var source = new SourceManager(sourceCode);
            var db = new DocumentBuilder(source, new HTMLDocument());
            return db.HtmlResult;
        }

        /// <summary>
        /// Builds a new HTMLDocument with the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <returns>The constructed HTML document.</returns>
        public static HTMLDocument Html(Uri url)
        {
            var stream = Builder.Stream(url);
            var source = new SourceManager(stream);
            var db = new DocumentBuilder(source, new HTMLDocument());
            return db.HtmlResult;
        }

        /// <summary>
        /// Builds a new HTMLDocument with the given network stream.
        /// </summary>
        /// <param name="networkStream">The stream of chars to use as source code.</param>
        /// <returns>The constructed HTML document.</returns>
        public static HTMLDocument Html(Stream networkStream)
        {
            var source = new SourceManager(networkStream);
            var db = new DocumentBuilder(source, new HTMLDocument());
            return db.HtmlResult;
        }

        /// <summary>
        /// Builds a list of nodes according with 8.4 Parsing HTML fragments.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="context">The context node to use.</param>
        /// <returns>A list of parsed nodes.</returns>
        public static NodeList HtmlFragment(String sourceCode, Node context = null)
        {
            var source = new SourceManager(sourceCode);
            var doc = new HTMLDocument();
            var db = new DocumentBuilder(source, doc);

            if (context != null)
            {
                if (context.OwnerDocument != null && context.OwnerDocument.QuirksMode != QuirksMode.Off)
                    doc.QuirksMode = context.OwnerDocument.QuirksMode;

                //    Note: For performance reasons, an implementation that does not report errors and that uses
                //          the actual state machine described in this specification directly could use the
                //          PLAINTEXT state instead of the RAWTEXT and script data states where those are mentioned
                //          in the list above. Except for rules regarding parse errors, they are equivalent, since
                //          there is no appropriate end tag token in the fragment case, yet they involve far
                //          fewer state transitions.

                ((HtmlParser)db.parser).SwitchToFragment(context);
                return db.HtmlResult.DocumentElement.ChildNodes;
            }

            return db.HtmlResult.ChildNodes;
        }

        #endregion

        #region CSS Construction

        /// <summary>
        /// Builds a new CSSStyleSheet with the given source code string.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static CSSStyleSheet Css(String sourceCode)
        {
            var source = new SourceManager(sourceCode);
            var db = new DocumentBuilder(source, new CSSStyleSheet());
            return db.CssResult;
        }

        /// <summary>
        /// Builds a new CSSStyleSheet with the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static CSSStyleSheet Css(Uri url)
        {
            var stream = Builder.Stream(url);
            var source = new SourceManager(stream);
            var db = new DocumentBuilder(source, new CSSStyleSheet());
            return db.CssResult;
        }

        /// <summary>
        /// Builds a new CSSStyleSheet with the given network stream.
        /// </summary>
        /// <param name="networkStream">The stream of chars to use as source code.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static CSSStyleSheet Css(Stream networkStream)
        {
            var source = new SourceManager(networkStream);
            var db = new DocumentBuilder(source, new CSSStyleSheet());
            return db.CssResult;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Called once a helper class finds a parse error.
        /// </summary>
        /// <param name="sender">The helper that encountered the error.</param>
        /// <param name="e">The arguments passed from the helper instance.</param>
        void ParseErrorOccurred(object sender, ParseErrorEventArgs e)
        {
            Debug.WriteLine(e);
        }

        #endregion
    }
}
