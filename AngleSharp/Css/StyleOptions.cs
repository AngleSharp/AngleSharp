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
        /// Gets or sets the element that triggered the evaluation.
        /// </summary>
        public IElement Element
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title of the stylesheet.
        /// </summary>
        public String Title
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
        /// Gets or sets the configuration for the stylesheet.
        /// </summary>
        public IConfiguration Configuration
        {
            get;
            set;
        }
    }
}
