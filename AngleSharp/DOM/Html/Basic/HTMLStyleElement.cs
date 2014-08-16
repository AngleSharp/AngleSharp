namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML style element.
    /// </summary>
    sealed class HTMLStyleElement : HTMLElement, IHtmlStyleElement
    {
        #region Fields

        IStyleSheet _sheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates an HTML style element.
        /// </summary>
        internal HTMLStyleElement()
            : base(Tags.Style, NodeFlags.Special | NodeFlags.LiteralText)
        {
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
        public IStyleSheet Sheet
        {
            get { return _sheet; }
        }

        /// <summary>
        /// Gets or sets if the style is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { if (_sheet != null) return _sheet.IsDisabled; else return false; }
            set { if (_sheet != null) _sheet.IsDisabled = value; }
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

        #region Internal methods

        public override void Close()
        {
            _sheet = Owner.Options.ParseStyling(source: TextContent, owner: this, type: Type);
        }

        /// <summary>
        /// Called if an attribute changed, has been added or removed.
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        protected override void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.Media, StringComparison.Ordinal))
            {
                if (_sheet != null)
                    _sheet.Media.MediaText = Media;
            }
            else
                base.OnAttributeChanged(name);
        }

        #endregion
    }
}
