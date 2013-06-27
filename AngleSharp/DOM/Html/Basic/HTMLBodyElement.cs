using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML body element.
    /// </summary>
    [DOM("HTMLBodyElement")]
    public sealed class HTMLBodyElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The body tag.
        /// </summary>
        internal const String Tag = "body";

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
        [DOM("aLink")]
        public String ALink
        {
            get { return GetAttribute("alink"); }
            set { SetAttribute("alink", value); }
        }

        /// <summary>
        /// Gets or sets the URI of the background texture tile image.
        /// </summary>
        [DOM("background")]
        public String Background
        {
            get { return GetAttribute("background"); }
            set { SetAttribute("background", value); }
        }

        /// <summary>
        /// Gets or sets the document background color.
        /// </summary>
        [DOM("bgColor")]
        public String BgColor
        {
            get { return GetAttribute("bgcolor"); }
            set { SetAttribute("bgcolor", value); }
        }

        /// <summary>
        /// Gets or sets color of links that are not active and unvisited.
        /// </summary>
        [DOM("link")]
        public String Link
        {
            get { return GetAttribute("link"); }
            set { SetAttribute("link", value); }
        }

        /// <summary>
        /// Gets or sets document text color.
        /// </summary>
        [DOM("text")]
        public String Text
        {
            get { return GetAttribute("text"); }
            set { SetAttribute("text", value); }
        }

        /// <summary>
        /// Gets or sets color of links that have been visited by the user.
        /// </summary>
        [DOM("vLink")]
        public String VLink
        {
            get { return GetAttribute("vlink"); }
            set { SetAttribute("vlink", value); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
