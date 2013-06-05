using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represent a stylesheet object.
    /// </summary>
    public class StyleSheet
    {
        #region Members

        string _type;
        Node _owner;
        StyleSheet _parent;
        string _href;
        string _title;
        MediaList _media;
        bool _disabled;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new style sheet.
        /// </summary>
        public StyleSheet()
        {
            _disabled = false;
            _media = new MediaList();
        }

        /// <summary>
        /// Creates a new style sheet included in another stylesheet.
        /// </summary>
        /// <param name="parent">The parent of the current stylesheet.</param>
        public StyleSheet(StyleSheet parent)
            : this()
        {
            _owner = parent._owner;
            _type = parent._type;
            _parent = parent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the style sheet language for this style sheet.
        /// </summary>
        public string Type 
        {
            get { return _type; }
            internal set { _type = value; }
        }

        /// <summary>
        /// Gets or sets if the stylesheet is applied to the document. Modifying this attribute may cause a new resolution
        /// of style for the document. If the media doesn't apply to the current user agent, the disabled attribute is ignored.
        /// </summary>
        public bool Disabled
        {
            get { return _disabled; }
            set { _disabled = value; }
        }

        /// <summary>
        /// Gets the node that associates this style sheet with the document.
        /// </summary>
        public Node OwnerNode
        {
            get { return _owner; }
            internal set { _owner = value; }
        }

        /// <summary>
        /// Gets the parent stylesheet for style sheet languages that support the concept of style sheet inclusion.
        /// </summary>
        public StyleSheet ParentStyleSheet
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the value of the attribute, which is its location. For inline style sheets, the value of this attribute is null.
        /// </summary>
        public string Href
        {
            get { return _href; }
            internal set { _href = value; }
        }

        /// <summary>
        /// Gets the advisory title. The title is often specified in the ownerNode.
        /// </summary>
        public string Title
        {
            get { return _title; }
            internal set { _title = value; }
        }

        /// <summary>
        /// Gets the intended destination media for style information. The media is often specified in the ownerNode. If no
        /// media has been specified, the MediaList is empty.
        /// </summary>
        public MediaList Media
        {
            get { return _media; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clones the current stylesheet.
        /// </summary>
        /// <returns>The cloned stylesheet.</returns>
        internal virtual StyleSheet Clone()
        {
            //TODO
            return this;
        }

        #endregion
    }
}
