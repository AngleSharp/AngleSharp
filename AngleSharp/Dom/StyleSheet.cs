namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represent a stylesheet object.
    /// </summary>
    abstract class StyleSheet : IStyleSheet
    {
        #region Fields

        readonly MediaList _media;
        String _url;
        String _title;
        IElement _owner;
        IStyleSheet _parent;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new style sheet.
        /// </summary>
        internal StyleSheet()
        {
            _media = new MediaList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the style sheet language for this style sheet.
        /// </summary>
        public virtual String Type
        {
            get { return _owner != null ? (_owner.GetAttribute(null, AttributeNames.Type) ?? String.Empty) : String.Empty; }
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
        /// Gets the element that associates this style sheet with the 
        /// document.
        /// </summary>
        public IElement OwnerNode
        {
            get { return _owner; }
            internal set { _owner = value; }
        }

        /// <summary>
        /// Gets the parent stylesheet for style sheet languages that support
        /// the concept of style sheet inclusion.
        /// </summary>
        public IStyleSheet Parent
        {
            get { return _parent; }
            internal set { _parent = value; }
        }

        /// <summary>
        /// Gets the value of the attribute, which is its location. For inline
        /// style sheets, the value of this attribute is null.
        /// </summary>
        public String Href
        {
            get { return _url; }
            internal set { _url = value; }
        }

        /// <summary>
        /// Gets the advisory title. The title is often specified in the
        /// ownerNode.
        /// </summary>
        public String Title
        {
            get { return _title; }
            internal set { _title = value; }
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
    }
}
