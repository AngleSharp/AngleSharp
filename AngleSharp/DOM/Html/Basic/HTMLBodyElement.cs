using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML body element.
    /// </summary>
    public sealed class HTMLBodyElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The body tag.
        /// </summary>
        internal const string Tag = "body";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a HTML body element.
        /// </summary>
        internal HTMLBodyElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the color of active links (after mouse-button down, but before mouse-button up). 
        /// </summary>
        public string ALink
        {
            get { return GetAttribute("alink"); }
            set { SetAttribute("alink", value); }
        }

        /// <summary>
        /// Gets or sets the URI of the background texture tile image.
        /// </summary>
        public string Background
        {
            get { return GetAttribute("background"); }
            set { SetAttribute("background", value); }
        }

        /// <summary>
        /// Gets or sets the document background color.
        /// </summary>
        public string BgColor
        {
            get { return GetAttribute("bgcolor"); }
            set { SetAttribute("bgcolor", value); }
        }

        /// <summary>
        /// Gets or sets color of links that are not active and unvisited.
        /// </summary>
        public string Link
        {
            get { return GetAttribute("link"); }
            set { SetAttribute("link", value); }
        }

        /// <summary>
        /// Gets or sets document text color.
        /// </summary>
        public string Text
        {
            get { return GetAttribute("text"); }
            set { SetAttribute("text", value); }
        }

        /// <summary>
        /// Gets or sets color of links that have been visited by the user.
        /// </summary>
        public string VLink
        {
            get { return GetAttribute("vlink"); }
            set { SetAttribute("vlink", value); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
