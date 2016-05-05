namespace AngleSharp.Services.Styling
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Transport object for evaluating stylesheets.
    /// </summary>
    public sealed class StyleOptions
    {
        /// <summary>
        /// Creates new style options for the given context.
        /// </summary>
        /// <param name="context">The context to use.</param>
        public StyleOptions(IBrowsingContext context)
        {
            Context = context;
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

        /// <summary>
        /// Gets the current browsing context.
        /// </summary>
        public IBrowsingContext Context
        {
            get;
            private set;
        }
    }
}
