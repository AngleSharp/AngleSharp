namespace AngleSharp.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Html;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represent a stylesheet object.
    /// </summary>
    [DebuggerStepThrough]
    abstract class StyleSheet : IStyleSheet
    {
        #region Fields

        readonly MediaList _media;
        readonly String _url;
        readonly IElement _owner;
        readonly IStyleSheet _parent;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new style sheet.
        /// </summary>
        /// <param name="media">The media list to use.</param>
        /// <param name="url">The url of the stylesheet.</param>
        /// <param name="parent">The parent stylesheet.</param>
        internal StyleSheet(MediaList media, String url, IStyleSheet parent)
            : this(media, url, parent != null ? parent.OwnerNode : null)
        {
            _parent = parent;
        }

        /// <summary>
        /// Creates a new style sheet.
        /// </summary>
        /// <param name="media">The media list to use.</param>
        /// <param name="url">The url of the stylesheet.</param>
        /// <param name="owner">The owner element.</param>
        internal StyleSheet(MediaList media, String url, IElement owner)
        {
            _media = media;
            _owner = owner;
            _url = url;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the style sheet language for this style sheet.
        /// </summary>
        public virtual String Type
        {
            get { return _owner != null ? _owner.GetAttribute(AttributeNames.Type) : null; }
        }

        /// <summary>
        /// Gets or sets if the stylesheet is applied to the document.
        /// Modifying this attribute may cause a new resolution of style for
        /// the document. If the media doesn't apply to the current user agent,
        /// the disabled attribute is ignored.
        /// </summary>
        public Boolean IsDisabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the element that associates this style sheet with the document.
        /// </summary>
        public IElement OwnerNode
        {
            get { return _owner; }
        }

        /// <summary>
        /// Gets the parent stylesheet for style sheet languages that support
        /// the concept of style sheet inclusion.
        /// </summary>
        public IStyleSheet Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the value of the attribute, which is its location. For inline
        /// style sheets, the value of this attribute is null.
        /// </summary>
        public String Href
        {
            get { return _url; }
        }

        /// <summary>
        /// Gets the advisory title. The title is specified in the ownerNode.
        /// </summary>
        public String Title
        {
            get { return _owner != null ? _owner.GetAttribute(AttributeNames.Title) : null; }
        }

        /// <summary>
        /// Gets the intended destination media for style information. The
        /// media is often specified in the ownerNode. If no media has
        /// been specified, the MediaList is empty.
        /// </summary>
        public IMediaList Media
        {
            get { return _media; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The source code snippet.</returns>
        public abstract String ToCss(IStyleFormatter formatter);

        #endregion
    }
}
