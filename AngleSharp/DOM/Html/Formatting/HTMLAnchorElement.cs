using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an anchor element.
    /// </summary>
    public sealed class HTMLAnchorElement : HTMLFormattingElement
    {
        #region Constants

        /// <summary>
        /// The a tag.
        /// </summary>
        internal const string Tag = "a";

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
        public string Href
        {
            get { return GetAttribute("href"); }
            set { SetAttribute("href", value); }
        }

        #endregion

        #region Design properties

        /// <summary>
        /// Gets or sets if the link has been visited.
        /// </summary>
        internal bool IsVisited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the link is currently active.
        /// </summary>
        internal bool IsActive
        {
            get;
            set;
        }

        #endregion
    }
}
