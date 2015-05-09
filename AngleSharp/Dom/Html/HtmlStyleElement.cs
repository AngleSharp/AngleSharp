namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;


    /// <summary>
    /// Represents the HTML style element.
    /// </summary>
    sealed class HtmlStyleElement : HtmlElement, IHtmlStyleElement
    {
        #region Fields

        IStyleSheet _sheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates an HTML style element.
        /// </summary>
        public HtmlStyleElement(Document owner, String prefix = null)
            : base(owner, Tags.Style, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
            RegisterAttributeObserver(AttributeNames.Media, UpdateMedia);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the style is scoped.
        /// </summary>
        public Boolean IsScoped
        {
            get { return GetOwnAttribute(AttributeNames.Scoped) != null; }
            set { SetOwnAttribute(AttributeNames.Scoped, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the associated style sheet.
        /// </summary>
        public IStyleSheet Sheet
        {
            get { return _sheet ?? (_sheet = CreateSheet()); }
        }

        /// <summary>
        /// Gets or sets if the style is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return GetOwnAttribute(AttributeNames.Disabled).ToBoolean(); }
            set 
            {
                SetOwnAttribute(AttributeNames.Disabled, value ? String.Empty : null);

                if (_sheet != null) 
                    _sheet.IsDisabled = value; 
            }
        }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        public String Media
        {
            get { return GetOwnAttribute(AttributeNames.Media); }
            set { SetOwnAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        public String Type
        {
            get { return GetOwnAttribute(AttributeNames.Type); }
            set { SetOwnAttribute(AttributeNames.Type, value); }
        }

        #endregion

        #region Internal methods

        internal override void NodeIsInserted(Node newNode)
        {
            base.NodeIsInserted(newNode);
            UpdateSheet();
        }

        internal override void NodeIsRemoved(Node removedNode, Node oldPreviousSibling)
        {
            base.NodeIsRemoved(removedNode, oldPreviousSibling);
            UpdateSheet();
        }

        #endregion

        #region Helpers

        void UpdateMedia(String value)
        {
            if (_sheet != null)
                _sheet.Media.MediaText = value;
        }

        void UpdateSheet()
        {
            if (_sheet != null)
                _sheet = CreateSheet();
        }

        IStyleSheet CreateSheet()
        {
            var config = Owner.Options;
            var engine = config.GetStyleEngine(Type ?? MimeTypes.Css);

            if (engine == null)
                return null;

            var options = new StyleOptions
            {
                Element = this,
                IsDisabled = IsDisabled,
                Title = Title,
                IsAlternate = false,
                Configuration = config
            };
            return engine.Parse(TextContent, options);
        }

        #endregion
    }
}
