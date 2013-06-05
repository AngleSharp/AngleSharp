using System;
using System.Diagnostics;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using System.IO;
using AngleSharp.Html;

namespace AngleSharp
{
    /// <summary>
    /// The class to parse the HTML and construct the DOM.
    /// </summary>
    public class DocumentBuilder
    {
        #region Members

        HtmlSource _;
        HTMLDocument document;
        Tokenization tokenizer;
        TreeConstruction tree;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new builder with the specified source.
        /// </summary>
        /// <param name="source">The code manager.</param>
        private DocumentBuilder(HtmlSource source, HTMLDocument document)
        {
            _ = source;
            this.document = document;
            tokenizer = new Tokenization(source);
            tree = new TreeConstruction(document, tokenizer);
            tokenizer.ErrorOccurred += ParseErrorOccurred;
            tree.ErrorOccurred += ParseErrorOccurred;
            tree.Stop += StopParsing;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Builds a new HTMLDocument with the given source code string.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <returns>The constructed HTML document.</returns>
        public static HTMLDocument Build(string sourceCode)
        {
            var source = new HtmlSource(sourceCode);
            var db = new DocumentBuilder(source, new HTMLDocument());
            db.tokenizer.Start();
            return db.document;
        }

        /// <summary>
        /// Builds a new HTMLDocument with the given source code stream.
        /// </summary>
        /// <param name="sourceCode">The stream of chars to use as source code.</param>
        /// <returns>The constructed HTML document.</returns>
        public static HTMLDocument Build(Stream sourceCode)
        {
            var source = new HtmlSource(sourceCode);
            var db = new DocumentBuilder(source, new HTMLDocument());
            db.tokenizer.Start();
            return db.document;
        }

        /// <summary>
        /// Builds content for a new HTMLDocument with the given source code.
        /// </summary>
        /// <param name="document">The document to use as a basis.</param>
        /// <param name="url">The URL where to get the source code.</param>
        /// <returns>The constructed HTML document.</returns>
        internal static HTMLDocument Build(HTMLDocument document, string url)
        {
            var ms = new MemoryStream();
            var source = new HtmlSource(ms);
            var db = new DocumentBuilder(source, document);
            db.tokenizer.Start();
            return db.document;
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

        /// <summary>
        /// Called once the tree builder requests the immediate stop of the parsing.
        /// </summary>
        /// <param name="sender">The tree builder instance.</param>
        /// <param name="e">Nothing.</param>
        void StopParsing(object sender, EventArgs e)
        {
            tokenizer.Stop();
        }

        #endregion
    }
}
