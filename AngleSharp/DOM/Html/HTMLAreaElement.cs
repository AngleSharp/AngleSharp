using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the area element.
    /// </summary>
    public sealed class HTMLAreaElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The area tag.
        /// </summary>
        public const string Tag = "area";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new area element.
        /// </summary>
        public HTMLAreaElement()
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

        #region Internal properties

        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
