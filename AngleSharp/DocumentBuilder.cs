using System;
using System.Diagnostics;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using System.IO;
using AngleSharp.Html;
using AngleSharp.DOM.Collections;

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
        HtmlTokenizer tokenizer;
        TreeConstruction tree;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new builder with the specified source.
        /// </summary>
        /// <param name="source">The code manager.</param>
        /// <param name="document">The document to fill.</param>
        private DocumentBuilder(HtmlSource source, HTMLDocument document)
        {
            _ = source;
            this.document = document;
            tokenizer = new HtmlTokenizer(source);
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
        public static HTMLDocument Html(string sourceCode)
        {
            var source = new HtmlSource(sourceCode);
            var db = new DocumentBuilder(source, new HTMLDocument());
            db.tokenizer.Start();
            return db.document;
        }

        /// <summary>
        /// Builds a new HTMLDocument with the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <returns>The constructed HTML document.</returns>
        public static HTMLDocument Html(Uri url)
        {
            var stream = Builder.Stream(url);
            var source = new HtmlSource(stream);
            var db = new DocumentBuilder(source, new HTMLDocument());
            db.tokenizer.Start();
            return db.document;
        }

        /// <summary>
        /// Builds a new HTMLDocument with the given network stream.
        /// </summary>
        /// <param name="networkStream">The stream of chars to use as source code.</param>
        /// <returns>The constructed HTML document.</returns>
        public static HTMLDocument Html(Stream networkStream)
        {
            var source = new HtmlSource(networkStream);
            var db = new DocumentBuilder(source, new HTMLDocument());
            db.tokenizer.Start();
            return db.document;
        }

        /// <summary>
        /// Builds a list of nodes according with 8.4 Parsing HTML fragments.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="context">The context node to use.</param>
        /// <returns>A list of parsed nodes.</returns>
        public static NodeList HtmlFragment(string sourceCode, Node context = null)
        {
            var source = new HtmlSource(sourceCode);
            var doc = new HTMLDocument();
            var nb = new DocumentBuilder(source, doc);

            if (context != null)
            {
                if (context.OwnerDocument != null && context.OwnerDocument.QuirksMode != QuirksMode.Off)
                    doc.QuirksMode = context.OwnerDocument.QuirksMode;

                switch (context.NodeName)
                {
                    case HTMLTitleElement.Tag:
                    case HTMLTextAreaElement.Tag:
                        nb.tokenizer.Switch(ContentModel.RCData);
                        break;

                    case HTMLStyleElement.Tag:
                    case "xmp":
                    case HTMLIFrameElement.Tag:
                    case HTMLNoElement.NoEmbedTag:
                    case HTMLNoElement.NoFramesTag:
                        nb.tokenizer.Switch(ContentModel.Rawtext);
                        break;

                    case HTMLScriptElement.Tag:
                        nb.tokenizer.Switch(ContentModel.Script);
                        break;

                    case HTMLNoElement.NoScriptTag:
                        if (nb.document.IsScripting)
                            nb.tokenizer.Switch(ContentModel.Rawtext);
                        break;

                    case "plaintext":
                        nb.tokenizer.Switch(ContentModel.Plaintext);
                        break;
                }

                //    Note: For performance reasons, an implementation that does not report errors and that uses
                //          the actual state machine described in this specification directly could use the
                //          PLAINTEXT state instead of the RAWTEXT and script data states where those are mentioned
                //          in the list above. Except for rules regarding parse errors, they are equivalent, since
                //          there is no appropriate end tag token in the fragment case, yet they involve far
                //          fewer state transitions.

                nb.tree.SwitchToFragment(context);
                nb.tokenizer.Start();
                return doc.DocumentElement.ChildNodes;
            }

            nb.tokenizer.Start();
            return doc.ChildNodes;
        }

        /// <summary>
        /// Builds content for a new HTMLDocument with the given source code.
        /// </summary>
        /// <param name="document">The document to use as a basis.</param>
        /// <param name="url">The URL where to get the source code.</param>
        /// <returns>The constructed HTML document.</returns>
        internal static HTMLDocument Html(HTMLDocument document, string url)
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
