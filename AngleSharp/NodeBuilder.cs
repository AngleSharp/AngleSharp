using System;
using System.Diagnostics;
using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Html;
using AngleSharp.Html;

namespace AngleSharp
{
    /// <summary>
    /// The class to parse the HTML and construct a list of nodes (Fragment case).
    /// </summary>
    public class NodeBuilder
    {
        #region Members

        HtmlSource _;
        HTMLDocument document;
        Tokenization tokenizer;
        TreeConstruction tree;

        #endregion

        #region ctor

        private NodeBuilder(HtmlSource source)
        {
            _ = source;
            document = new HTMLDocument();
            tokenizer = new Tokenization(source);
            tree = new TreeConstruction(document, tokenizer);

            tokenizer.ErrorOccurred += ParseErrorOccurred;
            tree.ErrorOccurred += ParseErrorOccurred;
            tree.Stop += StopParsing;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Builds a list of nodes according with 8.4 Parsing HTML fragments.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <returns>A list of parsed nodes.</returns>
        public static NodeList Build(string sourceCode, Node context = null)
        {
            var source = new HtmlSource(sourceCode);
            var nb = new NodeBuilder(source);

            if (context != null)
            {
                if (context.OwnerDocument != null && context.OwnerDocument.QuirksMode != QuirksMode.Off)
                    nb.document.QuirksMode = context.OwnerDocument.QuirksMode;

                switch(context.NodeName)
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
                        if(nb.document.IsScripting)
                            nb.tokenizer.Switch(ContentModel.Rawtext);
                        break;

                    case "plaintext":
                        nb.tokenizer.Switch(ContentModel.Plaintext);
                        break;

    //    Note: For performance reasons, an implementation that does not report errors and that uses the actual state machine described in this specification
    //          directly could use the PLAINTEXT state instead of the RAWTEXT and script data states where those are mentioned in the list above. Except for
    //          rules regarding parse errors, they are equivalent, since there is no appropriate end tag token in the fragment case, yet they involve far
    //          fewer state transitions.
                }

                nb.tree.SwitchToFragment(context);
                nb.tokenizer.Start();
                return nb.document.DocumentElement.ChildNodes;
            }

            nb.tokenizer.Start();
            return nb.document.ChildNodes;
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
