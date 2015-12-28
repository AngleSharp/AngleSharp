namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Html;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represent a stylesheet object.
    /// </summary>
    [DebuggerStepThrough]
    abstract class StyleSheet : CssNode, IStyleSheet
    {
        #region Fields

        readonly MediaList _media;
        readonly String _url;
        readonly IElement _owner;
        readonly IStyleSheet _parent;

        #endregion

        #region ctor

        internal StyleSheet(MediaList media, String url, IStyleSheet parent)
            : this(media, url, parent != null ? parent.OwnerNode : null)
        {
            _parent = parent;
        }

        internal StyleSheet(MediaList media, String url, IElement owner)
        {
            _media = media;
            _owner = owner;
            _url = url;
        }

        #endregion

        #region Properties

        public virtual String Type
        {
            get { return _owner != null ? _owner.GetAttribute(AttributeNames.Type) : null; }
        }

        public Boolean IsDisabled
        {
            get;
            set;
        }

        public IElement OwnerNode
        {
            get { return _owner; }
        }

        public IStyleSheet Parent
        {
            get { return _parent; }
        }

        public String Href
        {
            get { return _url; }
        }

        public String Title
        {
            get { return _owner != null ? _owner.GetAttribute(AttributeNames.Title) : null; }
        }

        public IMediaList Media
        {
            get { return _media; }
        }

        #endregion
    }
}
