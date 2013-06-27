using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML source element.
    /// </summary>
    [DOM("HTMLSourceElement")]
    public sealed class HTMLSourceElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The source tag.
        /// </summary>
        internal const string Tag = "source";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML source element.
        /// </summary>
        internal HTMLSourceElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the URL for the media resource.
        /// </summary>
        public string Src
        {
            get { return GetAttribute("src"); }
            set { SetAttribute("src", value); }
        }

        /// <summary>
        /// Gets or sets the intended type of the media resource.
        /// </summary>
        public string Media
        {
            get { return GetAttribute("media"); }
            set { SetAttribute("media", value); }
        }

        /// <summary>
        /// Gets or sets the type of the media source.
        /// </summary>
        public string Type
        {
            get { return GetAttribute("type"); }
            set { SetAttribute("type", value); }
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
