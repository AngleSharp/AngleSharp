namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represent a stylesheet object.
    /// </summary>
    [DomName("StyleSheet")]
    public class StyleSheet
    {
        #region Fields

        Element _owner;
        StyleSheet _parent;
        MediaList _media;
        String _url;
        IConfiguration _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new style sheet.
        /// </summary>
        internal StyleSheet()
        {
            _media = new MediaList();
        }

        /// <summary>
        /// Creates a new style sheet included in another stylesheet.
        /// </summary>
        /// <param name="parent">The parent of the current stylesheet.</param>
        internal StyleSheet(StyleSheet parent)
            : this()
        {
            _owner = parent._owner;
            _parent = parent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the style sheet language for this style sheet.
        /// </summary>
        [DomName("type")]
        public String Type
        {
            get { return _owner != null ? (_owner.GetAttribute(AttributeNames.Type) ?? String.Empty) : String.Empty; }
        }

        /// <summary>
        /// Gets or sets if the stylesheet is applied to the document. Modifying this attribute may cause a new resolution
        /// of style for the document. If the media doesn't apply to the current user agent, the disabled attribute is ignored.
        /// </summary>
        [DomName("disabled")]
        public Boolean Disabled
        {
            get { return _owner != null ? (_owner.GetAttribute(AttributeNames.Disabled) != null) : false; }
            set { if (_owner != null) _owner.SetAttribute(AttributeNames.Disabled, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the element that associates this style sheet with the document.
        /// </summary>
        [DomName("ownerNode")]
        public Element OwnerNode
        {
            get { return _owner; }
            internal set { _owner = value; }
        }

        /// <summary>
        /// Gets the parent stylesheet for style sheet languages that support the concept of style sheet inclusion.
        /// </summary>
        [DomName("parentStyleSheet")]
        public StyleSheet ParentStyleSheet
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the value of the attribute, which is its location. For inline style sheets, the value of this attribute is null.
        /// </summary>
        [DomName("href")]
        public String Href
        {
            get { return _owner != null ? (_owner.GetAttribute(AttributeNames.Href) ?? String.Empty) : (_url ?? String.Empty); }
            internal set { _url = value; }
        }

        /// <summary>
        /// Gets the advisory title. The title is often specified in the ownerNode.
        /// </summary>
        [DomName("title")]
        public String Title
        {
            get { return _owner != null ? (_owner.GetAttribute(AttributeNames.Title) ?? String.Empty) : String.Empty; }
        }

        /// <summary>
        /// Gets the intended destination media for style information. The media is often specified in the ownerNode. If no
        /// media has been specified, the MediaList is empty.
        /// </summary>
        [DomName("media")]
        public MediaList Media
        {
            get { return _media; }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets or sets the document options.
        /// </summary>
        internal IConfiguration Options
        {
            get 
            {
                if (_options != null)
                    return _options;
                else if (_owner != null && _owner.Owner != null)
                    return _owner.Owner.Options;
                else if (_parent != null)
                    return _parent.Options;

                return Configuration.Default; 
            }
            set { _options = value; }
        }

        #endregion
    }
}
