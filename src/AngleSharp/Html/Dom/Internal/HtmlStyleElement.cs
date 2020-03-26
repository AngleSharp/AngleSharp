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

        public HtmlStyleElement(Document owner, String prefix = null)
            : base(owner, TagNames.Style, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion

        #region Properties

        public Boolean IsScoped
        {
            get => this.GetBoolAttribute(AttributeNames.Scoped);
            set => this.SetBoolAttribute(AttributeNames.Scoped, value);
        }

        public IStyleSheet Sheet => _sheet;

        public Boolean IsDisabled
        {
            get => this.GetBoolAttribute(AttributeNames.Disabled);
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
            get => this.GetOwnAttribute(AttributeNames.Media);
            set => this.SetOwnAttribute(AttributeNames.Media, value);
        }

        public String Type
        {
            get => this.GetOwnAttribute(AttributeNames.Type);
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();
            UpdateSheet();
        }

        internal void UpdateMedia(String value)
        {
            if (_sheet != null)
            {
                _sheet.Media.MediaText = value;
            }
        }

        #endregion

        #region Helpers

        protected override void NodeIsInserted(Node newNode)
        {
            base.NodeIsInserted(newNode);
            UpdateSheet();
        }

        protected override void NodeIsRemoved(Node removedNode, Node oldPreviousSibling)
        {
            base.NodeIsRemoved(removedNode, oldPreviousSibling);
            UpdateSheet();
        }

        private void UpdateSheet()
        {
            var document = Owner;

            if (document != null)
            {
                var context = Context;
                var type = Type ?? MimeTypeNames.Css;
                var engine = context.GetStyling(type);

                if (engine != null)
                {
                    var task = CreateSheetAsync(engine, document);
                    document.DelayLoad(task);
                }
            }
        }

        private async Task CreateSheetAsync(IStylingService engine, IDocument document)
        {
            var cancel = CancellationToken.None;
            var response = VirtualResponse.Create(res => res.Content(TextContent).Address(default(Url)));
            var options = new StyleOptions(document)
            {
                Element = this,
                IsDisabled = IsDisabled,
                IsAlternate = false,
            };
            var task = engine.ParseStylesheetAsync(response, options, cancel);
            _sheet = await task.ConfigureAwait(false);
            UpdateMedia(Media);
        }

        #endregion
    }
}
