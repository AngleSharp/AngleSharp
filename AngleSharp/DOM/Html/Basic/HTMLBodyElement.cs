namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML body element.
    /// </summary>
    sealed class HTMLBodyElement : HTMLElement, IImplClosed, IHtmlBodyElement
    {
        #region ctor

        /// <summary>
        /// Creates a HTML body element.
        /// </summary>
        internal HTMLBodyElement()
        {
            _name = Tags.Body;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the color of active links (after mouse-button down, but before mouse-button up). 
        /// </summary>
        public String ALink
        {
            get { return GetAttribute("alink"); }
            set { SetAttribute("alink", value); }
        }

        /// <summary>
        /// Gets or sets the URI of the background texture tile image.
        /// </summary>
        public String Background
        {
            get { return GetAttribute("background"); }
            set { SetAttribute("background", value); }
        }

        /// <summary>
        /// Gets or sets the document background color.
        /// </summary>
        public String BgColor
        {
            get { return GetAttribute("bgcolor"); }
            set { SetAttribute("bgcolor", value); }
        }

        /// <summary>
        /// Gets or sets color of links that are not active and unvisited.
        /// </summary>
        public String Link
        {
            get { return GetAttribute("link"); }
            set { SetAttribute("link", value); }
        }

        /// <summary>
        /// Gets or sets document text color.
        /// </summary>
        public String Text
        {
            get { return GetAttribute("text"); }
            set { SetAttribute("text", value); }
        }

        /// <summary>
        /// Gets or sets color of links that have been visited by the user.
        /// </summary>
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

        #region Events from IDL

        public event EventListener Printed;

        public event EventListener Printing;

        public event UnloadEventListener Unloading;

        public event EventListener HashChanged;

        public event EventListener MessageReceived;

        public event EventListener WentOffline;

        public event EventListener WentOnline;

        public event EventListener PageHidden;

        public event EventListener PageShown;

        public event EventListener PopState;

        public event EventListener Storage;

        public event EventListener Unloaded;

        #endregion
    }
}
