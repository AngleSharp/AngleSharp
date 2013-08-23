using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an anchor element.
    /// </summary>
    [DOM("HTMLAnchorElement")]
    public sealed class HTMLAnchorElement : HTMLFormattingElement
    {
        #region Constants

        /// <summary>
        /// The a tag.
        /// </summary>
        internal const String Tag = "a";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new anchor element.
        /// </summary>
        internal HTMLAnchorElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the href attribute.
        /// </summary>
        public String Href
        {
            get { return HyperRef(GetAttribute("href")); }
            set { SetAttribute("href", value); }
        }

        #endregion

        #region Design properties

        /// <summary>
        /// Gets or sets if the link has been visited.
        /// </summary>
        internal Boolean IsVisited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the link is currently active.
        /// </summary>
        internal Boolean IsActive
        {
            get;
            set;
        }

        #endregion
    }
}
