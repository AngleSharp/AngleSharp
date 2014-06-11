namespace AngleSharp.Tools
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Html;
    using System;

    /// <summary>
    /// Represents a sample browsing Window implementation for
    /// automated tests, analysis and as a useful playground.
    /// </summary>
    public class AnalysisWindow : IWindow
    {
        #region ctor

        /// <summary>
        /// Creates a new analysis windows context without any starting
        /// document. The height and width properties are also not set.
        /// </summary>
        public AnalysisWindow()
        {
        }

        /// <summary>
        /// Creates a new analysis window starting with the given document.
        /// </summary>
        /// <param name="document">The document to use.</param>
        public AnalysisWindow(Document document)
        {
            Document = document;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a reference to the document that the window contains.
        /// </summary>
        public IDocument Document
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the window.
        /// </summary>
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the height of the outside of the browser window.
        /// </summary>
        public Int32 OuterHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the width of the outside of the browser window.
        /// </summary>
        public Int32 OuterWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the horizontal distance of the left border of the user's
        /// browser from the left side of the screen.
        /// </summary>
        public Int32 ScreenX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the vertical distance of the top border of the user's
        /// browser from the top side of the screen.
        /// </summary>
        public Int32 ScreenY
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gives the values of all the CSS properties of an element after
        /// applying the active stylesheets and resolving any basic computation
        /// those values may contain.
        /// </summary>
        /// <param name="element">The element to compute the styles for.</param>
        /// <param name="pseudo">The optional pseudo selector to use.</param>
        /// <returns>The style declaration describing the element.</returns>
        public CSSStyleDeclaration GetComputedStyle(IElement element, String pseudo = null)
        {
            var document = Document as HTMLDocument;

            if (document == null)
                throw new ArgumentException("A valid HTML document is required for computing the style of an element.");

            var obj = element;

            // if pseudo is :before OR ::before then use the corresponding pseudo-element
            // else if pseudo is :after OR ::after then use the corresponding pseudo-element

            var style = new CSSStyleDeclaration { IsReadOnly = true };

            foreach (var stylesheet in document.StyleSheets)
            {
                if (!stylesheet.Disabled && stylesheet.Media.Validate(this) && stylesheet is CSSStyleSheet)
                {
                    foreach (var rule in ((CSSStyleSheet)stylesheet).CssRules)
                        rule.ComputeStyle(style, this, element);
                }
            }

            //style.InheritFrom(element);
            return style;
        }

        #endregion
    }
}
