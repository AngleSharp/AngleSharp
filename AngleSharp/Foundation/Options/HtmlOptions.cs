using System;

namespace AngleSharp
{
    /// <summary>
    /// A set of defined options / flags for the parsing.
    /// </summary>
    sealed class HtmlOptions
    {
        static readonly HtmlOptions _default;

        static HtmlOptions()
        {
            _default = new HtmlOptions
            {
                IsScripting = false,
                IsStyling = true,
                IsEmbedded = false
            };
        }

        /// <summary>
        /// Gets the default options.
        /// </summary>
        public static HtmlOptions Default
        {
            get { return _default; }
        }

        /// <summary>
        /// Gets or sets if scripting is active and allowed.
        /// </summary>
        public Boolean IsScripting
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if styling is active and allowed.
        /// </summary>
        public Boolean IsStyling
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the document is embedded by an iframe srcdoc element.
        /// </summary>
        public Boolean IsEmbedded
        {
            get;
            set;
        }
    }
}
