namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services.Styling;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the HTML style element.
    /// </summary>
    sealed class HtmlStyleElement : HtmlElement, IHtmlStyleElement
    {
        #region Fields

        IStyleSheet _sheet;

        #endregion

        #region ctor

        static HtmlStyleElement()
        {
            RegisterCallback<HtmlStyleElement>(AttributeNames.Media, (element, value) => element.UpdateMedia(value));
        }

        public HtmlStyleElement(Document owner, String prefix = null)
            : base(owner, TagNames.Style, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion

        #region Properties

        public Boolean IsScoped
        {
            get { return this.HasOwnAttribute(AttributeNames.Scoped); }
            set { this.SetOwnAttribute(AttributeNames.Scoped, value ? String.Empty : null); }
        }

        public IStyleSheet Sheet
        {
            get { return _sheet; }
        }

        public Boolean IsDisabled
        {
            get { return this.GetOwnAttribute(AttributeNames.Disabled).ToBoolean(); }
            set 
            {
                this.SetOwnAttribute(AttributeNames.Disabled, value ? String.Empty : null);

                if (_sheet != null) 
                    _sheet.IsDisabled = value; 
            }
        }

        public String Media
        {
            get { return this.GetOwnAttribute(AttributeNames.Media); }
            set { this.SetOwnAttribute(AttributeNames.Media, value); }
        }

        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();
            UpdateSheet();
        }

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
            {
                _sheet.Media.MediaText = value;
            }
        }

        void UpdateSheet()
        {
            var document = Owner;

            if (document != null)
            {
                var config = document.Options;
                var context = document.Context;
                var type = Type ?? MimeTypeNames.Css;
                var engine = config.GetStyleEngine(type);

                if (engine != null)
                {
                    var task = CreateSheetAsync(engine, context);
                    document.DelayLoad(task);
                }
            }
        }

        async Task CreateSheetAsync(IStyleEngine engine, IBrowsingContext context)
        {
            var cancel = CancellationToken.None;
            var response = VirtualResponse.Create(res => res.Content(TextContent).Address(default(Url)));
            var options = new StyleOptions(context)
            {
                Element = this,
                IsDisabled = IsDisabled,
                IsAlternate = false
            };
            var task = engine.ParseStylesheetAsync(response, options, cancel);
            _sheet = await task.ConfigureAwait(false);
        }

        #endregion
    }
}
