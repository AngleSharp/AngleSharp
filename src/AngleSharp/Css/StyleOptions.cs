namespace AngleSharp.Css
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Transport object for evaluating stylesheets.
    /// </summary>
    public sealed class StyleOptions
    {
        /// <summary>
        /// Creates new style options for the given document.
        /// </summary>
        /// <param name="document">The document to use.</param>
        public StyleOptions(IDocument document)
        {
            Document = document;
        }

        /// <summary>
        /// Gets the parent document for hosting the style sheet.
        /// </summary>
        public IDocument Document
        {
            get;
        }

        /// <summary>
        /// Gets or sets the element that triggered the evaluation.
        /// </summary>
        public IElement Element
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the stylesheet is disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the stylesheet is an alternate.
        /// </summary>
        public Boolean IsAlternate
        {
            get;
            set;
        }
    }
}
