namespace AngleSharp.Html.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the HTML style element.
    /// </summary>
    sealed class HtmlStyleElement : HtmlElement, IHtmlStyleElement
    {
        #region Fields

        private IStyleSheet _sheet;

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
            get { return this.GetBoolAttribute(AttributeNames.Scoped); }
            set { this.SetBoolAttribute(AttributeNames.Scoped, value); }
        }

        public IStyleSheet Sheet
        {
            get { return _sheet; }
        }

        public Boolean IsDisabled
        {
            get { return this.GetBoolAttribute(AttributeNames.Disabled); }
            set 
            {
                this.SetBoolAttribute(AttributeNames.Disabled, value);

                if (_sheet != null)
                {
                    _sheet.IsDisabled = value;
                }
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

        private void UpdateMedia(String value)
        {
            if (_sheet != null)
            {
                _sheet.Media.MediaText = value;
            }
        }

        private void UpdateSheet()
        {
            var document = Owner;

            if (document != null)
            {
                var context = Context;
                var type = Type ?? MimeTypeNames.Css;
                var engine = context.GetStyleEngine(type);

                if (engine != null)
                {
                    var task = CreateSheetAsync(engine, context);
                    document.DelayLoad(task);
                }
            }
        }

        private async Task CreateSheetAsync(IStyleEngine engine, IBrowsingContext context)
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
