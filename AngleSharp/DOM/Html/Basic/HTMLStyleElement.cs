namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Css;
    using System;

    /// <summary>
    /// Represents the HTML style element.
    /// </summary>
    [DomName("HTMLStyleElement")]
    public sealed class HTMLStyleElement : HTMLElement, IStyleSheet, IHtmlStyleElement
    {
        #region Fields

        readonly CSSStyleSheet _sheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates an HTML style element.
        /// </summary>
        internal HTMLStyleElement()
        {
            _name = Tags.Style;
            _sheet = new CSSStyleSheet();
            _sheet.OwnerNode = this;
            _children.ElementsChanged += OnChildrenChanged;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the style is scoped.
        /// </summary>
        public Boolean IsScoped
        {
            get { return GetAttribute(AttributeNames.Scoped) != null; }
            set { SetAttribute(AttributeNames.Scoped, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the associated style sheet.
        /// </summary>
        public StyleSheet Sheet
        {
            get { return _sheet; }
        }

        /// <summary>
        /// Gets or sets if the style is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return Sheet.Disabled; }
            set { Sheet.Disabled = value; }
        }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        public String Media
        {
            get { return GetAttribute(AttributeNames.Media); }
            set { SetAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a special textual representation of the node.
        /// </summary>
        /// <returns>A string containing only (rendered) text.</returns>
        public override String ToText()
        {
            return String.Empty;
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

        #region Internal methods

        void OnChildrenChanged(Object sender, EventArgs e)
        {
            _sheet.ReevaluateFromSource(TextContent);
        }

        /// <summary>
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.Media, StringComparison.Ordinal))
                _sheet.Media.MediaText = Media;
            else
                base.OnAttributeChanged(name);
        }

        #endregion
    }
}
