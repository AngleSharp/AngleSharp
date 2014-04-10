namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Css;
    using System;

    /// <summary>
    /// Represents the HTML link element.
    /// </summary>
    [DOM("HTMLLinkElement")]
    public sealed class HTMLLinkElement : HTMLElement, IStyleSheet
    {
        #region Fields

        CSSStyleSheet _sheet;
        String _buffer;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML link element.
        /// </summary>
        internal HTMLLinkElement()
        {
            _name = Tags.Link;
            _sheet = new CSSStyleSheet();
            _sheet.OwnerNode = this;
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

        #region Design properties

        /// <summary>
        /// Gets or sets if the link has been visited.
        /// </summary>
        internal Boolean IsVisited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the link is currently active.
        /// </summary>
        internal Boolean IsActive
        {
            get;
            set;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the URI for the target resource.
        /// </summary>
        [DOM("href")]
        public String Href
        {
            get { return HyperRef(GetAttribute(AttributeNames.Href)); }
            set { SetAttribute(AttributeNames.Href, value); }
        }

        /// <summary>
        /// Gets or sets the language code for the linked resource.
        /// </summary>
        [DOM("hreflang")]
        public String Hreflang
        {
            get { return GetAttribute(AttributeNames.HrefLang); }
            set { SetAttribute(AttributeNames.HrefLang, value); }
        }

        /// <summary>
        /// Gets or sets the character encoding for the target resource.
        /// </summary>
        [DOM("charset")]
        public String Charset
        {
            get { return GetAttribute(AttributeNames.Charset); }
            set { SetAttribute(AttributeNames.Charset, value); }
        }

        /// <summary>
        /// Gets or sets the forward relationship of the linked resource from the document to the resource.
        /// </summary>
        [DOM("rel")]
        public RelType Rel
        {
            get { return ToEnum(GetAttribute(AttributeNames.Rel), RelType.None); }
            set { SetAttribute(AttributeNames.Rel, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the reverse relationship of the linked resource from the document to the resource.
        /// </summary>
        [DOM("rev")]
        public String Rev
        {
            get { return GetAttribute(AttributeNames.Rev); }
            set { SetAttribute(AttributeNames.Rev, value); }
        }

        /// <summary>
        /// Gets or sets if the stylesheet is enabled or disabled.
        /// </summary>
        [DOM("disabled")]
        public Boolean Disabled
        {
            get { return Sheet.Disabled; }
            set { Sheet.Disabled = value; }
        }

        /// <summary>
        /// Gets or sets the name of the target frame to which the resource applies.
        /// </summary>
        [DOM("target")]
        public String Target
        {
            get { return GetAttribute(AttributeNames.Target); }
            set { SetAttribute(AttributeNames.Target, value); }
        }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        [DOM("media")]
        public String Media
        {
            get { return GetAttribute(AttributeNames.Media); }
            set { SetAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets the associated stylesheet.
        /// </summary>
        [DOM("sheet")]
        public StyleSheet Sheet
        {
            get { return Rel == RelType.Stylesheet ? _sheet : null; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.Media, StringComparison.Ordinal))
                _sheet.Media.MediaText = Media;
            else if (name.Equals(AttributeNames.Href, StringComparison.Ordinal) || name.Equals(AttributeNames.Rel, StringComparison.Ordinal))
            {
                var href = Href;

                if (href != null && Rel == RelType.Stylesheet)
                {
                    if (_buffer != href)
                    {
                        _buffer = href;
                        _sheet.ReevaluateFromUrl(href);
                    }
                }
            }
            else
                base.OnAttributeChanged(name);
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// Specifies the possible values for the rel attribute.
        /// </summary>
        public enum RelType : ushort
        {
            /// <summary>
            /// No particular relation.
            /// </summary>
            None,
            /// <summary>
            /// The rel=prefetch value.
            /// </summary>
            Prefetch,
            /// <summary>
            /// The rel=icon value.
            /// </summary>
            Icon,
            /// <summary>
            /// The rel=pingback value.
            /// </summary>
            Pingback,
            /// <summary>
            /// The rel=stylesheet value.
            /// </summary>
            Stylesheet,
            /// <summary>
            /// The rel=alternate value.
            /// </summary>
            Alternate,
            /// <summary>
            /// The rel=canonical value.
            /// </summary>
            Canonical,
            /// <summary>
            /// The rel=archives value.
            /// </summary>
            Archives,
            /// <summary>
            /// The rel=author value.
            /// </summary>
            Author,
            /// <summary>
            /// The rel=first value.
            /// </summary>
            First,
            /// <summary>
            /// The rel=help value.
            /// </summary>
            Help,
            /// <summary>
            /// The rel=sidebar value.
            /// </summary>
            Sidebar,
            /// <summary>
            /// The rel=tag value.
            /// </summary>
            Tag,
            /// <summary>
            /// The rel=search value.
            /// </summary>
            Search,
            /// <summary>
            /// The rel=index value.
            /// </summary>
            Index,
            /// <summary>
            /// The rel=license value.
            /// </summary>
            License,
            /// <summary>
            /// The rel=up value.
            /// </summary>
            Up,
            /// <summary>
            /// The rel=next value.
            /// </summary>
            Next,
            /// <summary>
            /// The rel=last value.
            /// </summary>
            Last,
            /// <summary>
            /// The rel=prev value.
            /// </summary>
            Prev
        }

        #endregion
    }
}
