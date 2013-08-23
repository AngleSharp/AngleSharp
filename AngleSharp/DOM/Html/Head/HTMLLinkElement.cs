using AngleSharp.DOM.Css;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML link element.
    /// </summary>
    [DOM("HTMLLinkElement")]
    public sealed class HTMLLinkElement : HTMLElement, IStyleSheet
    {
        #region Constant

        /// <summary>
        /// The link tag.
        /// </summary>
        internal const String Tag = "link";

        #endregion

        #region Members

        CSSStyleSheet _sheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML link element.
        /// </summary>
        internal HTMLLinkElement()
        {
            _name = Tag;
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
            get { return HyperRef(GetAttribute("href")); }
            set { SetAttribute("href", value); }
        }

        /// <summary>
        /// Gets or sets the language code for the linked resource.
        /// </summary>
        [DOM("hreflang")]
        public String Hreflang
        {
            get { return GetAttribute("hreflang"); }
            set { SetAttribute("hreflang", value); }
        }

        /// <summary>
        /// Gets or sets the character encoding for the target resource.
        /// </summary>
        [DOM("charset")]
        public String Charset
        {
            get { return GetAttribute(HtmlEncoding.CHARSET); }
            set { SetAttribute(HtmlEncoding.CHARSET, value); }
        }

        /// <summary>
        /// Gets or sets the forward relationship of the linked resource from the document to the resource.
        /// </summary>
        [DOM("rel")]
        public RelType Rel
        {
            get { return ToEnum(GetAttribute("rel"), RelType.Stylesheet); }
            set { SetAttribute("rel", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the reverse relationship of the linked resource from the document to the resource.
        /// </summary>
        [DOM("rev")]
        public String Rev
        {
            get { return GetAttribute("rev"); }
            set { SetAttribute("rev", value); }
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
            get { return GetAttribute("target"); }
            set { SetAttribute("target", value); }
        }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        [DOM("media")]
        public String Media
        {
            get { return GetAttribute("media"); }
            set { SetAttribute("media", value); }
        }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get { return GetAttribute("type"); }
            set { SetAttribute("type", value); }
        }

        /// <summary>
        /// Gets the associated stylesheet.
        /// </summary>
        [DOM("sheet")]
        public StyleSheet Sheet
        {
            get { return _sheet; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals("media", StringComparison.Ordinal))
                _sheet.Media.MediaText = Media;
            else if (name.Equals("href", StringComparison.Ordinal))
                _sheet.ReevaluateFromUrl(Href);
            else
                base.OnAttributeChanged(name);
        }

        /// <summary>
        /// Registers the node at the given document.
        /// </summary>
        /// <param name="document">The document where to register.</param>
        protected override void Register(Document document)
        {
            if (Rel == RelType.Stylesheet)
                document.StyleSheets.Add(Sheet);
        }

        /// <summary>
        /// Unregisters the node at the given document.
        /// </summary>
        /// <param name="document">The document where to unregister.</param>
        protected override void Unregister(Document document)
        {
            if (Rel == RelType.Stylesheet)
                document.StyleSheets.Remove(Sheet);
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// Specifies the possible values for the rel attribute.
        /// </summary>
        public enum RelType : ushort
        {
            Prefetch,
            Icon,
            Pingback,
            Stylesheet,
            Alternate,
            Canonical,
            Archives,
            Author,
            First,
            Help,
            Sidebar,
            Tag,
            Search,
            Index,
            License,
            Up,
            Next,
            Last,
            Prev
        }

        #endregion
    }
}
